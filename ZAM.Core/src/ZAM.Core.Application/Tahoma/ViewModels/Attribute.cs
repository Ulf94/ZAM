namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record Attribute
{
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
    [JsonPropertyName("type")] public int Type { get; init; } = default;
    [JsonPropertyName("values")] public IReadOnlyList<string> Values { get; init; } = new List<string>();
}
