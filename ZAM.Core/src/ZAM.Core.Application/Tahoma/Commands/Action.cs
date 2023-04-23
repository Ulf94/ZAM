namespace ZAM.Core.Application.Tahoma.Commands;

public sealed record Action : IRequest
{
    public string Command { get; init; } = string.Empty;
    public string DeviceUrl { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
}
