namespace ZAM.Core.Application.PI.Queries;

public sealed record GetRoomTemperature : IRequest
{
    public int RoomNumber { get; set; } = default;
}
