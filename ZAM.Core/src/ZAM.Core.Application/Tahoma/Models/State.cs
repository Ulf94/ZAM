namespace ZAM.Core.Application.Tahoma.Models;

public sealed record State
{
    public string Name { get; init; } = string.Empty;
    public int Type { get; init; } = default;
    public string Value { get; init; } = string.Empty;
}
