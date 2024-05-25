using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers;

[Authorize]
[ApiController]
[Route("admin")]
public class AdminController : ControllerBase
{
    [HttpGet]
    public OkResult Get()
    {
        return Ok();
    }
}
