namespace ZAM.Core.Infrastructure.Tahoma.Services;

using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZAM.Core.Application.Common.Exceptions;
using ZAM.Core.Application.Tahoma.Services;
using ZAM.Core.Application.Tahoma.ViewModels;
using ZAM.Core.Infrastructure.Common.Exceptions;
using ZAM.Core.Infrastructure.Tahoma.Extensions;

internal class TahomaService : ITahomaService
{
    private const string COOKIE_NAME = "COOKIE";
    private const string GET_COOKIE_URI = "https://ha101-1.overkiz.com/enduser-mobile-web/enduserAPI/login";
    private const string GET_DEVICES_URI = "https://ha101-1.overkiz.com/enduser-mobile-web/enduserAPI/setup/devices";
    private const string SEND_ACTION_URI = "https://ha101-1.overkiz.com/enduser-mobile-web/enduserAPI/exec/apply";

    private readonly HttpClient client;
    private readonly IConfiguration configuration;
    private readonly ILogger<TahomaService> logger;

    public TahomaService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<TahomaService> logger)
    {
        this.configuration = configuration;
        this.client = httpClientFactory.CreateClient(TahomaOptions.SectionName);
        this.logger = logger;
    }

    public async Task<List<Device>> GetDevices()
    {
        this.logger.LogInformation("Getting devices information");

        var result = await this.Send(HttpMethod.Get, GET_DEVICES_URI);

        var text = await result.Content.ReadAsStringAsync();

        var devices = JsonSerializer.Deserialize<List<Device>>(text)!;

        return devices;
    }

    public Task<string> GetLoginCookie()
    {
        if (this.client.DefaultRequestHeaders.Contains(COOKIE_NAME))
        {
            this.logger.LogInformation("Cookie already fetched");

            var fetchedCookie = this.client.DefaultRequestHeaders.GetValues(COOKIE_NAME).First();

            return Task.FromResult(fetchedCookie);
        }

        var userId = this.GetCredentials()["userId"];
        var userPassword = this.GetCredentials()["userPassword"];

        this.logger.LogInformation("Getting cookie with {UserName} and {Password}", userId.ObfuscateCredentials(), userPassword.ObfuscateCredentials());

        var httpContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new("userId", userId),
            new("userPassword", userPassword),
        });

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

        var cookieValue = cookies!.ExtractCookieValue();

        this.client.DefaultRequestHeaders.Add("Cookie", $"JSESSIONID={cookieValue}");

        this.logger.LogInformation("Cookie fetched");

        return Task.FromResult(cookieValue);
    }

    public void SendAction(string label, string command, string deviceUrl)
    {
        this.logger.LogInformation("Sending action {Command} to {Label}", command, label);

        var action = new SendAction()
        {
            Label = label,
            Actions = new List<ActionModel>
            {
                new()
                {
                    Commands = new()
                    {
                        new()
                        {
                            Name = command,
                        },
                    },
                    DeviceURL = deviceUrl,
                },
            },
        };

        var json = JsonSerializer.Serialize(action);

        _ = this.Send(HttpMethod.Post, SEND_ACTION_URI, json);
    }

    private Dictionary<string, string> GetCredentials()
    {
        var userId = this.configuration.GetSection(TahomaOptions.SectionName).Get<TahomaOptions>()?.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            userId = Environment.GetEnvironmentVariable("CONFIG_TAHOMA__USERID", EnvironmentVariableTarget.Machine);
        }

        var userPassword = this.configuration.GetSection(TahomaOptions.SectionName).Get<TahomaOptions>()?.UserPassword;

        if (string.IsNullOrWhiteSpace(userPassword))
        {
            userPassword = Environment.GetEnvironmentVariable("CONFIG_TAHOMA__USERPASSWORD", EnvironmentVariableTarget.Machine);
        }

        if (userId is null || userPassword is null)
        {
            throw new CredentialsNotFoundExceptions();
        }

        var credentials = new Dictionary<string, string>
        {
            { "userId", userId },
            { "userPassword", userPassword },
        };

        return credentials;
    }

    private Task<HttpResponseMessage> Send(HttpMethod httpMethod, string url, string content = "")
    {
        this.logger.LogInformation("Sending request with a cookie to an {Url}", url);

        var request = new HttpRequestMessage(httpMethod, url)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json"),
        };

        var result = this.client.Send(request);

        result.EnsureSuccessStatusCode();

        this.logger.LogInformation("Request to {url} succeeded", url);

        return Task.FromResult(result);
    }
}
