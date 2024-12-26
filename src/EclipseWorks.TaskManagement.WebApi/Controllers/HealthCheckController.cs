using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/health-check")]
public sealed class HealthCheckController : ControllerBase
{
    /// <summary>
    /// Performs a health check to verify the status of the application.
    /// </summary>
    /// <returns>An IActionResult indicating the health status of the application.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Check()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1));
        return Ok();
    }
}