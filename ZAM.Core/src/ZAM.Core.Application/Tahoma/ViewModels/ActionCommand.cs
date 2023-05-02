namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed record ActionCommand
{
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
}
