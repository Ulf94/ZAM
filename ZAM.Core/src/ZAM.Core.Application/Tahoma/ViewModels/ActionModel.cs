namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record ActionModel
{
    [JsonPropertyName("commands")] public List<ActionCommand> Commands { get; init; } = new();
    [JsonPropertyName("deviceURL")] public string DeviceURL { get; init; } = string.Empty;
}
