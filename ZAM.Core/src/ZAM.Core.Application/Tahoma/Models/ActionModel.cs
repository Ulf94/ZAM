namespace ZAM.Core.Application.Tahoma.Models;

public sealed record ActionModel
{
    public List<ActionCommand> Commands { get; init; } = new();
    public string Url { get; init; } = string.Empty;
}
