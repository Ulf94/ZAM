namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record Device
{
    [JsonPropertyName("attributes")] public IReadOnlyList<Attribute> Attributes { get; init; } = new List<Attribute>();
    [JsonPropertyName("available")] public bool Available { get; init; } = default;
    [JsonPropertyName("box")] public string Box { get; init; } = string.Empty;
    [JsonPropertyName("controllableName")] public string ControllableName { get; init; } = string.Empty;
    [JsonPropertyName("creationTime")] public long CreationTime { get; init; } = default;
    [JsonPropertyName("definition")] public Definition? Definition { get; init; } = default;
    [JsonPropertyName("deviceURL")] public string DeviceURL { get; init; } = string.Empty;
    [JsonPropertyName("enabled")] public bool Enabled { get; init; } = default;
    [JsonPropertyName("label")] public string Label { get; init; } = string.Empty;
    [JsonPropertyName("lastUpdatedTime")] public long LastUpdatedTime { get; init; } = default;
    [JsonPropertyName("oId")] public Guid Oid { get; init; } = Guid.Empty;
    [JsonPropertyName("placeId")] public Guid PlaceOID { get; init; } = Guid.Empty;
    [JsonPropertyName("Shortcut")] public bool Shortcut { get; init; } = default;
    [JsonPropertyName("states")] public IReadOnlyList<State>? States { get; init; } = new List<State>();
    [JsonPropertyName("type")] public int Type { get; init; } = default;
    [JsonPropertyName("uiClass")] public string UiClass { get; init; } = string.Empty;
    [JsonPropertyName("widget")] public string Widget { get; init; } = string.Empty;
}
