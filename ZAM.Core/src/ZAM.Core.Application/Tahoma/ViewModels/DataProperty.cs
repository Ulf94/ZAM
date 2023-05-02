namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record DataProperty
{
    [JsonPropertyName("qualifiedName")] public string QualifiedName { get; init; } = string.Empty;
    [JsonPropertyName("value")] public string Value { get; init; } = string.Empty;
}
