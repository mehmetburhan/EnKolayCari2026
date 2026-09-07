using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnKolayCari.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "ok", utc = DateTime.UtcNow });
}
