namespace ZAM.Core.Application.Tahoma.Commands;

using ViewModels;

public sealed record GetDevices : IRequest<Dictionary<string, List<Device>>>
{
}
