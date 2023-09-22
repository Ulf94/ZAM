namespace ZAM.Core.Infrastructure.Tahoma.Services;

using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;
using ZAM.Core.Application.Common.Exceptions;
using ZAM.Core.Application.Common.URIs;
using ZAM.Core.Application.Tahoma.Models;
using ZAM.Core.Application.Tahoma.Services;
using ZAM.Core.Application.Tahoma.ViewModels;
using ZAM.Core.Infrastructure.Tahoma.Extensions;

internal class TahomaService : ITahomaService
{
    private const string COOKIE_NAME = "Set-Cookie";
    private readonly HttpClient httpClient;
    private readonly ILogger<TahomaService> logger;
    private readonly ICredentialsProvider credentialsProvider;

    public TahomaService(IHttpClientFactory httpClientFactory, ILogger<TahomaService> logger, ICredentialsProvider credentialsProvider)
    {
        this.logger = logger;
        this.credentialsProvider = credentialsProvider;
        this.httpClient = httpClientFactory.CreateClient();
    }

    public async Task<Dictionary<string,List<Device>>> GetDevices()
    {
        this.logger.LogInformation("{TahomaService} Getting devices information", nameof(TahomaService));

        var result = await this.SendAsync(HttpMethod.Get, URIs.GET_DEVICES_URI);

        var text = await result.Content.ReadAsStringAsync();

        var allDevices = JsonSerializer.Deserialize<List<Device>>(text)!;

        Dictionary<string, List<Device>> groupedDevices = GroupDevicesByControllableName(allDevices);

        return groupedDevices;
    }

    public Task<string> GetLoginCookie()
    {
        var credentials = this.credentialsProvider.GetCredentials();

        this.logger.LogInformation("Getting cookie with {UserName} and {Password}", credentials.UserId.ObfuscateCredentials(), credentials.UserPassword.ObfuscateCredentials());

        FormUrlEncodedContent httpContent = PrepareHttpContent(credentials);

        HttpRequestMessage request = PrepareHttpReqest(httpContent);

        var response = this.httpClient.Send(request);

        response.EnsureSuccessStatusCode();

        if (response.StatusCode is not HttpStatusCode.OK)
        {
            this.logger.LogError("Cookie was not fetched");

            throw new CookieNotFetchedException();
        }

        var cookies = response.Headers.SingleOrDefault(header => header.Key == COOKIE_NAME).Value.FirstOrDefault();

        var cookieValue = cookies!.ExtractCookieValue();

        AddCookieToTheHttpClient(cookieValue);

        this.logger.LogInformation("Cookie fetched");

        return Task.FromResult(cookieValue);
    }

    public void SendTahomaAction(string label, string command, string deviceUrl)
    {
        this.logger.LogInformation("Sending action {Command} to {Label}", command, label);

        var action = new SendAction
        {
            Label = label,
            Actions = new()
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

        _ = this.SendAsync(HttpMethod.Post, URIs.SEND_ACTION_URI, json);
    }

    private void AddCookieToTheHttpClient(string cookieValue)
    {
        this.httpClient.DefaultRequestHeaders.Add("Cookie", $"JSESSIONID={cookieValue}");
    }

    private static HttpRequestMessage PrepareHttpReqest(FormUrlEncodedContent httpContent)
    {
        return new HttpRequestMessage(HttpMethod.Post, URIs.GET_COOKIE_URI)
        {
            Content = httpContent,
        };
    }

    private static FormUrlEncodedContent PrepareHttpContent(Credentials credentials)
    {
        return new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new("userId", credentials.UserId),
            new("userPassword", credentials.UserPassword),
        });
    }

    private async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string url, string content = "")
    {
        this.logger.LogInformation("Sending request with a cookie to an {Url}", url);

        var request = new HttpRequestMessage(httpMethod, url)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json"),
        };

        var result = await this.httpClient.SendAsync(request);

        result.EnsureSuccessStatusCode();

        this.logger.LogInformation("Request to {url} succeeded", url);

        return result;
    }

    private static Dictionary<string, List<Device>> GroupDevicesByControllableName(List<Device> allDevices)
    {
        Dictionary<string, List<Device>> groupedDevices = new();

        foreach (var device in allDevices)
        {
            if (groupedDevices.ContainsKey(device.ControllableName))
            {
                groupedDevices[device.ControllableName].Add(device);
            }
            else
            {
                groupedDevices.Add(device.ControllableName, new List<Device> { device });
            }
        }

        return groupedDevices;
    }
}
