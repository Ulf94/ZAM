namespace ZAM.Core.Application.UnitTests.Tahoma;

using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ZAM.Core.Application.Tahoma.Models;

[TestClass]
public sealed class SerializationTests
{
    private static readonly List<Command> expectedListOfCommands = new()
    {
        new Command()
        {
            CommandName = "discover",
            Nparams = 0,
        },
        new Command()
        {
            CommandName = "identify",
            Nparams = 0,
        },
        new Command()
        {
            CommandName = "setName",
            Nparams = 0,
        },
    };

    private static readonly List<DefinitionState> expectedListOfDefinitionStates = new()
    {
        new DefinitionState()
        {
            QualifiedName = "core:AvailabilityState",
            Type = "DiscreteState",
            Values = new List<string>()
            {
                "available",
                "unavailable",
            },
        },
        new DefinitionState()
        {
            QualifiedName = "core:NameState",
            Type = "DataState",
            Values = new List<string>(),
        },
        new DefinitionState()
        {
            QualifiedName = "core:RemovableState",
            Type = "DataState",
            Values = new List<string>(),
        },
    };

    private readonly Command expectedCommand = new()
    {
        CommandName = "getName",
        Nparams = 0,
    };

    private readonly List<State> expectedListOfStates = new()
    {
        new State()
        {
            Name = "internal:LightingLedPodModeState",
            Type = 2,
            Value = "1",
        },
        new State()
        {
            Name = "core:LocalIPv4AddressState",
            Type = 3,
            Value = "N/A",
        },
        new State()
        {
            Name = "core:NameState",
            Type = 3,
            Value = "Box",
        },
    };

    private Definition expectedDefinition = new Definition
    {
        Commands = expectedListOfCommands,
        DataProperties = new List<DataProperty>(),
        States = expectedListOfDefinitionStates,
        QualifiedName = "ogp:Bridge",
        Type = "ACTUATOR",
        UiClass = "ProtocolGateway",
        UiProfiles = new List<string>
        {
            "Specific",
        },
        WidgetName = "DynamicBridge",
    };

    [TestMethod]
    public void Should_Serialize_Command_Model()
    {
        //GIVEN
        var source = $@"{{
                    ""commandName"": ""getName"",
                    ""nparams"": 0
                }}";

        //WHEN
        Command deserialized = JsonConvert.DeserializeObject<Command>(source);

        //THEN
        deserialized.Should()
            .BeEquivalentTo(this.expectedCommand)
            ;
    }

    [TestMethod]
    public void Should_Serialize_Definition()
    {
        //GIVEN
        var source = @"{
            ""commands"": [
                {
                    ""commandName"": ""discover"",
                    ""nparams"": 0
                },
                {
                    ""commandName"": ""identify"",
                    ""nparams"": 0
                },
                {
                    ""commandName"": ""setName"",
                    ""nparams"": 0
                }
            ],
            ""states"": [
                {
                    ""type"": ""DiscreteState"",
                    ""values"": [
                        ""available"",
                        ""unavailable""
                    ],
                    ""qualifiedName"": ""core:AvailabilityState""
                },
                {
                    ""type"": ""DataState"",
                    ""qualifiedName"": ""core:NameState""
                },
                {
                    ""type"": ""DataState"",
                    ""qualifiedName"": ""core:RemovableState""
                }
            ],
            ""dataProperties"": [],
            ""widgetName"": ""DynamicBridge"",
            ""uiProfiles"": [
                ""Specific""
            ],
            ""uiClass"": ""ProtocolGateway"",
            ""qualifiedName"": ""ogp:Bridge"",
            ""type"": ""ACTUATOR""
        }";

        //WHEN
        var result = JsonConvert.DeserializeObject<Definition>(source);

        //THEN
        result.Should()
            .BeEquivalentTo(this.expectedDefinition)
            ;
    }

    [TestMethod]
    public void Should_Serialize_List_Of_Commands()
    {
        //GIVEN
        var source = @"[
                {
                    ""commandName"": ""discover"",
                    ""nparams"": 0
                },
                {
                    ""commandName"": ""identify"",
                    ""nparams"": 0
                },
                {
                    ""commandName"": ""setName"",
                    ""nparams"": 0
                }]";

        //WHEN
        var result = JsonConvert.DeserializeObject<List<Command>>(source);

        //THEN
        result.Should()
            .BeEquivalentTo(expectedListOfCommands)
            ;
    }

    [TestMethod]
    public void Should_Serialize_List_Of_States()
    {
        //GIVEN
        var source = @"[
                        {
                            ""name"": ""internal:LightingLedPodModeState"",
                            ""type"": 2,
                            ""value"": 1
                        },
                        {
                            ""name"": ""core:LocalIPv4AddressState"",
                            ""type"": 3,
                            ""value"": ""N/A""
                        },
                        {
                            ""name"": ""core:NameState"",
                            ""type"": 3,
                            ""value"": ""Box""
                        }
                        ]";

        //WHEN
        var result = JsonConvert.DeserializeObject<List<State>>(source);

        //THEN
        result.Should()
            .BeEquivalentTo(this.expectedListOfStates)
            ;
    }
}
