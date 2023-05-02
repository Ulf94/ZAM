namespace ZAM.Core.Application.UnitTests.Tahoma.Models;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZAM.Core.Application.Tahoma.ViewModels;

[TestClass]
public sealed class AttributeTests
{
    private const int EXPECTED_NUMBER_OF_PROPERTIES = 3;

    private readonly string[] expectedListOfNames = new[]
    {
        "Name",
        "Type",
        "Values",
    };

    private readonly PropertyInfo[] properties;

    public AttributeTests()
    {
        this.properties = typeof(Attribute).GetProperties();
    }

    [TestMethod]
    public void Should_Validate_Names_Of_Properties()
    {
        //GIVEN
        List<string> namesOfProperties = new();

        //WHEN
        namesOfProperties = this.properties.Select(property => property.Name).ToList();

        //THEN
        namesOfProperties.Should()
            .BeEquivalentTo(this.expectedListOfNames)
            ;
    }

    [TestMethod]
    public void Should_Validate_Number_Of_Properties()
    {
        //GIVEN
        var numberOfProperties = 0;

        //WHEN
        numberOfProperties = this.properties.Length;

        //THEN
        numberOfProperties.Should()
            .Be(EXPECTED_NUMBER_OF_PROPERTIES)
            ;
    }
}
