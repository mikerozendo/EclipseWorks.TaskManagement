using EclipseWorks.TaskManagement.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

// [Authorize(Roles = "gerente")]
[ApiController]
[Route("api/v1/analytics")]
public sealed class AnalyticsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("by-period/{daysAgo:int}")]
    public async Task<IActionResult> GetByPeriod(int daysAgo)
    {
        if (daysAgo <= 0)
            return BadRequest();
        
        var request = new GetAnalyticsForPastDaysRequest(daysAgo);
        var response = await mediator.Send(request);

        if (response.Resource is not null)
            return Ok(response.Resource);
        
        return NotFound("No data found");
    }
}