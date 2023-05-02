namespace ZAM.Core.Application.Tahoma.Commands;

using ZAM.Core.Application.Tahoma.ViewModels;

public sealed record GetDevices : IRequest<List<Device>>
{
}
