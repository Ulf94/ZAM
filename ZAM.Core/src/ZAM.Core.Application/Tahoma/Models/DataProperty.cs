namespace ZAM.Core.Application.Tahoma.Models;

public sealed record DataProperty
{
    public string QualifiedName { get; init; } = string.Empty;
    public int Value { get; init; } = default;
}
