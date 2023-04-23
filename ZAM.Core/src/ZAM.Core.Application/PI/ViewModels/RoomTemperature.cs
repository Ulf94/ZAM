namespace ZAM.Core.Application.PI.ViewModels;

using System.Text.Json.Serialization;

public sealed record RoomTemperature
{
    [JsonPropertyName("temperature")] public int Temperature { get; init; } = default;
}
