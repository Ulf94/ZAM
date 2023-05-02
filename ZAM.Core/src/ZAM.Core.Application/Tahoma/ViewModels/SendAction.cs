namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public sealed class SendAction
{
    [JsonPropertyName("actions")] public List<ActionModel> Actions { get; init; } = new();
    [JsonPropertyName("label")] public string Label { get; init; } = string.Empty;
}
