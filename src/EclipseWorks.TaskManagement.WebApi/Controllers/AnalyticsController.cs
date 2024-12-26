using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

// todo: [Authorize(Roles = "gerente")]
[ApiController]
[Route("api/v1/analytics")]
public sealed class AnalyticsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("last-thirty-days")]
    public async Task<IActionResult> GetByPeriod()
    {
        var response = await mediator.Send(new GetAnalyticsForPastDaysRequest());

        if (response.Success)
            return Ok(response.Resource);

        return Problem("No data found in the specified filter", statusCode: (int)response.HttpStatusCode);
    }
}