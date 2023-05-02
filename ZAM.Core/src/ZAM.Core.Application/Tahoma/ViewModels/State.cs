namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record State
{
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
    [JsonPropertyName("type")] public int Type { get; init; } = default;
    [JsonPropertyName("value")] public object? Value { get; init; } = null;
}
