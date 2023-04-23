namespace ZAM.Core.Application.Tahoma.Queries;

using ZAM.Core.Application.Tahoma.Models;

public sealed record GetDevices : IRequest<List<Device>>
{
}
