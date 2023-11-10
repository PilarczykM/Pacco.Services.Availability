using Convey.Types;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Pacco.Services.Availability.Api.Controllers;

[Route("")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult Index()
    {
        return Ok(HttpContext.RequestServices.GetService<AppOptions>().Name);
    }
}
