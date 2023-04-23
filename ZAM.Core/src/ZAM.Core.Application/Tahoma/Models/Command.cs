namespace ZAM.Core.Application.Tahoma.Models;

public sealed record Command
{
    public string CommandName { get; init; } = string.Empty;
    public int Nparams { get; init; } = default;
}
