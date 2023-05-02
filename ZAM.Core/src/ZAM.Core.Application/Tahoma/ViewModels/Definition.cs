namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record Definition
{
    [JsonPropertyName("commands")] public IReadOnlyList<Command> Commands { get; init; } = new List<Command>();
    [JsonPropertyName("dataProperties")] public IReadOnlyList<DataProperty> DataProperties { get; init; } = new List<DataProperty>();
    [JsonPropertyName("qualifiedName")] public string QualifiedName { get; init; } = string.Empty;
    [JsonPropertyName("states")] public IReadOnlyList<DefinitionState> States { get; init; } = new List<DefinitionState>();
    [JsonPropertyName("type")] public string Type { get; init; } = string.Empty;
    [JsonPropertyName("uiClass")] public string UiClass { get; init; } = string.Empty;
    [JsonPropertyName("uiProfiles")] public List<string> UiProfiles { get; init; } = new List<string>();
    [JsonPropertyName("widgetName")] public string WidgetName { get; init; } = string.Empty;
}
