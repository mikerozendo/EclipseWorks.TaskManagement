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
    /// <summary>
    /// Retrieves analytical data for the last thirty days period.
    /// </summary>
    /// <returns>
    /// An IActionResult containing the analytical data if successful;
    /// otherwise, an error response indicating that no data was found for the specified filter.
    /// </returns>
    [HttpGet]
    [Route("last-thirty-days")]
    [ProducesResponseType(typeof(ResourceQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResourceQueryResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByPeriod()
    {
        var response = await mediator.Send(new GetAnalyticsForPastDaysRequest());

        if (response.Success)
            return Ok(response.Resource);

        return Problem("No data found in the specified filter", statusCode: (int)response.HttpStatusCode);
    }
}