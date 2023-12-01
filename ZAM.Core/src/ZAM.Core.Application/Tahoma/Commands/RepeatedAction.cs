namespace ZAM.Core.Application.Tahoma.Commands;

public sealed record RepeatedAction : IRequest
{
    public string Command { get; init; } = string.Empty;
    public string DeviceUrl { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public int IntervalInMinutes { get; init; } = default;
}
