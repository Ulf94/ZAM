namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record Command
{
    [JsonPropertyName("commandName")] public string CommandName { get; init; } = string.Empty;
    [JsonPropertyName("nparams")] public int Nparams { get; init; } = default;
}
