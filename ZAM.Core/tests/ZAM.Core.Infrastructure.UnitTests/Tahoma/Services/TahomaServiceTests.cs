namespace ZAM.Core.Infrastructure.UnitTests.Tahoma.Services;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZAM.Core.Application.Tahoma.ViewModels;

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

    [TestMethod]
    public void On_Given_IGrouping_Collection_Returns_Dictionary_With_Typ_As_A_Key()
    {
        //GIVEN

        var listOfDevices = new List<Device>
        {
            new()
            {
                ControllableName = "internal:PodMiniComponent",
                DeviceURL= "internal://0407-2414-8233/pod/0",
                Label = "Box",

            },
            new()
            {
                ControllableName = "ogp:Bridge",
                DeviceURL= "ogp://0407-2414-8233/00000BE8",
                Label = "OGP KNX Bridge",

            },
            new()
            {
                ControllableName = "ogp:Bridge",
                DeviceURL= "ogp://0407-2414-8233/0003FEF3",
                Label = "OGP Sonos Bridge",

            },
            new()
            {
                ControllableName = "rts:RollerShutterRTSComponent",
                DeviceURL= "rts://0407-2414-8233/16718663",
                Label = "Kotłownia",

            },
            new()
            {
                ControllableName = "rts:RollerShutterRTSComponent",
                DeviceURL= "rts://0407-2414-8233/16722225",
                Label = "Kuchnia",

            },
        };

        //WHEN
        Dictionary<string, List<Device>> groupedDevices = new();

        foreach(var device in listOfDevices)
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

        //THEN
        groupedDevices.Should()
            .HaveCount(3)
            ;
    }
}
