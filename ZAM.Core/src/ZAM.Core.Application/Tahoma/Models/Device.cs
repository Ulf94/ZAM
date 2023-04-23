namespace ZAM.Core.Application.Tahoma.Models;

public sealed record Device
{
    public IReadOnlyList<Models.Attribute> Attributes { get; init; } = new List<Models.Attribute>();
    public bool Available { get; init; } = default;
    public string Box { get; init; } = string.Empty;
    public string ControllableName { get; init; } = string.Empty;
    public long CreationTime { get; init; } = default;
    public Definition? Definition { get; init; } = default;
    public string DeviceUrl { get; init; } = string.Empty;
    public bool Enabled { get; init; } = default;
    public string Label { get; init; } = string.Empty;
    public long LastUpdatedTime { get; init; } = default;
    public Guid Oid { get; init; } = Guid.Empty;
    public Guid PlaceOID { get; init; } = Guid.Empty;
    public bool Shortcut { get; init; } = default;

    public IReadOnlyList<State>? States { get; init; } = new List<State>();
    public int Type { get; init; } = default;
    public string UiClass { get; init; } = string.Empty;
    public string Widget { get; init; } = string.Empty;
}
