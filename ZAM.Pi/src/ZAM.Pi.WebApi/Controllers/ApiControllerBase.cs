namespace ZAM.Pi.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

public class ApiControllerBase : Controller
{
    protected ISender Mediator => (this.mediator ?? this.HttpContext.RequestServices.GetService<ISender>())!;
    private ISender? mediator;
}
