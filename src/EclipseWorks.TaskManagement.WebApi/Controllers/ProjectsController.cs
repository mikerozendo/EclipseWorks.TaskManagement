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

        if (response.Success)
        {
            return CreatedAtAction(
                nameof(GetById),
                new { projectId = ((ResourceCommandOnSuccessResponse)response).ResourceId }, response
            );
        }
        
        var error = (ResourceCommandOnErrorResponse)response;
        return Problem(error.Details, statusCode: (int)error.HttpStatusCode);
    }
}