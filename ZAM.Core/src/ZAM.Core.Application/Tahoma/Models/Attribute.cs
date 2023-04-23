namespace ZAM.Core.Application.Tahoma.Models;

public sealed record Attribute
{
    public string Name { get; init; } = string.Empty;
    public int Type { get; init; } = default;
    public IReadOnlyList<string> Values { get; init; } = new List<string>();
}
