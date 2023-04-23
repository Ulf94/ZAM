namespace ZAM.Core.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ZAM.Core.Application.PI.Queries;

[ApiController]
[Route("[controller]/[action]")]
public class PiController : ControllerBase
{
    private readonly ISender mediator;

    public PiController(ISender mediator)
        => (this.mediator) = (mediator);

    [HttpPost]
    public async Task<IActionResult> GetTemperature(GetRoomTemperature request, CancellationToken cancellationToken = default)
    {
        await this.mediator.Send(request, cancellationToken);

        return this.Ok();
    }
}
