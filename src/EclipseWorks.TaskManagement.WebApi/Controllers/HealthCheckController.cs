using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/health-check")]
public sealed class HealthCheckController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Check()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1));
        return Ok();
    }
}