namespace ZAM.Core.Infrastructure.Tahoma.Services;

using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZAM.Core.Application.Common.Exceptions;
using ZAM.Core.Application.Tahoma.Models;
using ZAM.Core.Application.Tahoma.Services;
using ZAM.Core.Infrastructure.Tahoma.Extensions;

internal class TahomaService : ITahomaService
{
    private const string GET_COOKIE_URI = "https://ha101-1.overkiz.com/enduser-mobile-web/enduserAPI/login";
    private const string GET_DEVICES_URI = "https://ha101-1.overkiz.com/enduser-mobile-web/enduserAPI/setup/devices";
    private const string SEND_ACTION_URI = "https://ha101-1.overkiz.com/enduser-mobile-web/enduserAPI/exec/apply";
    private const string TOKEN_URI = "https://ha101-1.overkiz.com/enduser-mobile-web/enduserAPI/config/0407-2414-8233/local/tokens/generate";

    private readonly IMemoryCache cache;
    private readonly HttpClient client;
    private readonly IConfiguration configuration;
    private readonly ILogger<TahomaService> logger;

    public TahomaService(IConfiguration configuration, IMemoryCache cache, IHttpClientFactory httpClientFactory, ILogger<TahomaService> logger)
    {
        this.configuration = configuration;
        this.cache = cache;
        this.client = httpClientFactory.CreateClient(TahomaOptions.SectionName);
        this.logger = logger;
    }

    public async Task<List<Device>> GetDevices()
    {
        this.logger.LogInformation("Getting devices information");
        var result = await this.GetAsyncWithCookie(HttpMethod.Get, GET_DEVICES_URI);

        var text = await result.Content.ReadAsStringAsync();

        var devices = JsonConvert.DeserializeObject<List<Device>>(text)!;

        return devices;
    }

    public async Task<string> GetLoginCookie()
    {
        var userId = this.configuration.GetSection(TahomaOptions.SectionName).Get<TahomaOptions>()!.UserId;
        var userPassword = this.configuration.GetSection(TahomaOptions.SectionName).Get<TahomaOptions>()!.UserPassword;

        var httpContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new("userId", userId),
            new("userPassword", userPassword),
        });

        this.logger.LogInformation("Getting cookie with {UserName} and {Password}", userId.ObfuscateCredentials(), userPassword.ObfuscateCredentials());

        var request = new HttpRequestMessage(HttpMethod.Post, GET_COOKIE_URI)
        {
            Content = httpContent,
        };

        var response = this.client.Send(request);

        response.EnsureSuccessStatusCode();

        if (response.StatusCode is not HttpStatusCode.OK)
        {
            this.logger.LogError("Cookie was not fetched");

            throw new CookieNotFetchedException();
        }

        var cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value.FirstOrDefault();

        var cookieValue = cookies!.GetCookieValue();

        this.cache.Set("cookieValue", cookieValue);

        this.logger.LogInformation("Cookie fetched");

        return cookieValue;
    }

    public async Task<Token> GetToken()
    {
        this.logger.LogInformation("Requesting for a token");

        var tokenExists = this.cache.TryGetValue("token", out Token? token);

        if (tokenExists)
        {
            this.logger.LogInformation("Cached token is {Token}", token);

            return token!;
        }

        var result = await this.GetAsyncWithCookie(HttpMethod.Get, TOKEN_URI);

        if (result.StatusCode is not HttpStatusCode.OK)
        {
            this.logger.LogError("Token was not fetched");

            throw new TokenNotFetchedException();
        }

        token = await result.Content.ReadFromJsonAsync<Token>();

        this.cache.Set("token", token);

        this.logger.LogInformation("Token fetched");

        return token!;
    }

    public async void SendAction(string label, string command, string deviceUrl)
    {
        this.logger.LogInformation("Sending action {Command} to {Label}", command, label);

        var action = new SendAction()
        {
            Label = label,
            Action = new List<ActionModel>
            {
                new()
                {
                    Commands = new()
                    {
                        new()
                        {
                            Names =
                            {
                                command,
                            },
                        },
                    },
                    Url = deviceUrl,
                },
            },
        };

        var cookieExists = this.cache.TryGetValue("cookieValue", out var cookie);

        if (cookieExists is false)
        {
            this.logger.LogError("Reading cookie value from cache failed");

            throw new CookieNotFoundException("");
        }

        var request = new HttpRequestMessage(HttpMethod.Post, SEND_ACTION_URI)
        {
            Content = JsonContent.Create(action),
        };

        request.Headers.Add("Cookie", $"JSESSIONID={cookie}");

        var result = await this.client.SendAsync(request);

        result.EnsureSuccessStatusCode();

        this.logger.LogInformation("Request to succeeded");
    }

    private async Task<HttpResponseMessage> GetAsyncWithCookie(HttpMethod httpMethod, string url)
    {
        this.logger.LogInformation("Sending request with a cookie to an {Url}", url);

        var request = new HttpRequestMessage(httpMethod, url)
        {
            Content = new StringContent("", Encoding.UTF8, "application/json"),
        };

        var cookieExists = this.cache.TryGetValue("cookieValue", out var cookie);

        if (cookieExists is false)
        {
            this.logger.LogError("Reading cookie value from cache failed");

            throw new CookieNotFoundException(url);
        }

        request.Headers.Add("Cookie", $"JSESSIONID={cookie}");

        var result = this.client.Send(request);

        result.EnsureSuccessStatusCode();

        this.logger.LogInformation("Request to {url} succeeded", url);

        return result;
    }
}
