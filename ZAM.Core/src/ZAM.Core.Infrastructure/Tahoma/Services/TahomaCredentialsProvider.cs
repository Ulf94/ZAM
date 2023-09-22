namespace ZAM.Core.Infrastructure.Tahoma.Services;

using Application.Tahoma.Models;
using Application.Tahoma.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.Extensions.Logging;
using ZAM.Core.Application.Common.Exceptions;
using ZAM.Core.Application.Common.URIs;
using Common.Exceptions;
using Extensions;

public sealed class TahomaCredentialsProvider : ICredentialsProvider
{
    private readonly IConfiguration configuration;
    private readonly ILogger<TahomaCredentialsProvider> logger;
    private readonly HttpClient httpClient;

    public TahomaCredentialsProvider(IConfiguration configuration, ILogger<TahomaCredentialsProvider> logger, IHttpClientFactory httpClientFactory)
    {
        this.configuration = configuration;
        this.httpClient = httpClientFactory.CreateClient();
        this.logger = logger;
    }

    public Credentials GetCredentials()
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

        var credentials = new Credentials()
        {
            UserId = userId,
            UserPassword = userPassword,
        };

        return credentials;
    }

    public string GetCookie(Credentials credentials)
    {
        this.logger.LogInformation("Getting cookie with {UserName} and {Password}", credentials.UserId.ObfuscateCredentials(), credentials.UserPassword.ObfuscateCredentials());

        var httpContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new("userId", credentials.UserId),
            new("userPassword", credentials.UserPassword),
        });

        var request = new HttpRequestMessage(HttpMethod.Post, URIs.GET_COOKIE_URI)
        {
            Content = httpContent,
        };

        var response = this.httpClient.Send(request);

        response.EnsureSuccessStatusCode();

        if (response.StatusCode is not HttpStatusCode.OK)
        {
            this.logger.LogError("Cookie was not fetched");

            throw new CookieNotFetchedException();
        }

        var cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value.FirstOrDefault();

        var cookieValue = cookies!.ExtractCookieValue();

        this.httpClient.DefaultRequestHeaders.Add("Cookie", $"JSESSIONID={cookieValue}");

        this.logger.LogInformation("Cookie fetched");

        return cookieValue;

    }

    public string GetCookie()
    {
        var credentials = this.GetCredentials();

        return this.GetCookie(credentials);

    }
}