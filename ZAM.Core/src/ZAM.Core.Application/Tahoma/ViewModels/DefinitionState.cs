namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record DefinitionState
{
    [JsonPropertyName("qualifiedName")] public string QualifiedName { get; init; } = string.Empty;
    [JsonPropertyName("type")] public string Type { get; init; } = string.Empty;
    [JsonPropertyName("values")] public IReadOnlyList<string> Values { get; init; } = new List<string>();
}
