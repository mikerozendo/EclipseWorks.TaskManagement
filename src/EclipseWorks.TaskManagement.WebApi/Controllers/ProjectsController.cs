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
            return NotFound();

        return Ok(response.Resource);
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var response = await mediator.Send(new CreateProjectRequest
        {
            Id = Guid.NewGuid(),
        });

        if (!response.Success)
            return Problem(response.Details, statusCode: (int)response.HttpStatusCode);
        
        return CreatedAtAction(
            nameof(GetById),
            new { projectId = response.Resource }, response
        );
    }

    [HttpPatch]
    [Route("{projectId:guid}/close")]
    public async Task<IActionResult> CloseProject([FromRoute] Guid projectId)
    {
        var response = await mediator.Send(new CloseProjectByIdRequest
        {
            ProjectId = projectId,
        });

        if (!response.Success)
            return Problem(response.Details, statusCode: (int)response.HttpStatusCode);

        return Accepted();
    }
}