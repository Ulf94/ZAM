namespace ZAM.Core.Application.Tahoma.Models;

public sealed class SendAction
{
    public List<ActionModel> Action { get; init; } = new();
    public string Label { get; init; } = string.Empty;
}
