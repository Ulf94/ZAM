namespace ZAM.Core.Infrastructure.UnitTests.Tahoma.Services;

using System.Linq;
using System.Net.Http;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TahomaServiceTests
{
    private const string COOKIE_NAME = "Cookie";
    private const string COOKIE_VALUE = "fetchedCookie";

    [TestMethod]
    public void On_Given_Another_Cookie_Value_Should_Return_Already_Fetched_Cookie()
    {
        //GIVEN
        var httpClient = new HttpClient();

        //WHEN
        httpClient.DefaultRequestHeaders.Add(COOKIE_NAME, COOKIE_VALUE);
        var alreadyFetchedCookie = httpClient.DefaultRequestHeaders.GetValues(COOKIE_NAME).First();

        //THEN
        alreadyFetchedCookie.Should()
            .Be(COOKIE_VALUE)
            ;
    }
}
