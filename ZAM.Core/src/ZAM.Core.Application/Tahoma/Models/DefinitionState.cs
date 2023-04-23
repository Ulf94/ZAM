namespace ZAM.Core.Application.Tahoma.Models;

public sealed record DefinitionState
{
    public string QualifiedName { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public IReadOnlyList<string> Values { get; init; } = new List<string>();
}
