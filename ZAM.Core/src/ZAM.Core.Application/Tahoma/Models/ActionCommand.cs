namespace ZAM.Core.Application.Tahoma.Models;

public sealed record ActionCommand
{
    public List<string> Names { get; init; } = new();
}
