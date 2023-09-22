namespace ZAM.Core.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Application.Tahoma.Commands;
using Application.Tahoma.ViewModels;

[ApiController]
[Route("[controller]/[action]")]
public class TahomaController : ControllerBase
{
    private readonly ISender mediator;

    public TahomaController(ISender mediator)
        => (this.mediator) = (mediator);

    [HttpPost(Name = "GetDevices")]
    public async Task<Dictionary<string, List<Device>>> GetDevices(CancellationToken cancellationToken = default)
    {
        var request = new GetDevices();
        var result = await this.mediator.Send(request, cancellationToken);

        return result;
    }

    [HttpPost(Name = "GetLoginCookie")]
    public async Task<string> GetLoginCookies(GetLoginCookie request, CancellationToken cancellationToken = default)
    {
        var result = await this.mediator.Send(request, cancellationToken);

        return result;
    }

    [HttpPost(Name = "SendAction")]
    public async Task SendAction(Action request, CancellationToken cancellationToken = default)
    {
        await this.mediator.Send(request, cancellationToken);
    }
}
