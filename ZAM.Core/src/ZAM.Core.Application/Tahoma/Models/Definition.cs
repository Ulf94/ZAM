namespace ZAM.Core.Application.Tahoma.Models;

public sealed record Definition
{
    public IReadOnlyList<Command> Commands { get; init; } = new List<Command>();
    public IReadOnlyList<DataProperty> DataProperties { get; init; } = new List<DataProperty>();
    public string QualifiedName { get; init; } = string.Empty;
    public IReadOnlyList<DefinitionState> States { get; init; } = new List<DefinitionState>();
    public string Type { get; init; } = string.Empty;
    public string UiClass { get; init; } = string.Empty;
    public List<string> UiProfiles { get; init; } = new List<string>();
    public string WidgetName { get; init; } = string.Empty;
}
