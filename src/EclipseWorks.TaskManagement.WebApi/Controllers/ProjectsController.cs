using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/projects")]
public sealed class ProjectsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{projectId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid projectId)
    {
        var response = await mediator.Send(new GetProjectByIdQueryRequest(projectId));

        if (response.Resource is null)
            NotFound();

        return Ok(response.Resource);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProjectRequest createProjectRequest)
    {
        var response = await mediator.Send(createProjectRequest);

        if (response.Success)
            return CreatedAtAction(nameof(GetById), response);

        var error = (ResourceCommandOnErrorResponse)response;
        return Problem(error.Details, statusCode: (int)error.HttpStatusCode);
    }
}