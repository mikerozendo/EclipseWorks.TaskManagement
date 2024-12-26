using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/projects")]
public sealed class ProjectsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves a project by its unique identifier.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to retrieve.</param>
    /// <returns>Returns an HTTP 200 status code with the project resource if found, or an HTTP 404 status code if not found.</returns>
    [HttpGet]
    [Route("{projectId:guid}")]
    [ProducesResponseType(typeof(ResourceQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid projectId)
    {
        var response = await mediator.Send(new GetProjectByIdQueryRequest(projectId));

        if (response.Resource is null)
            return NotFound();

        return Ok(response.Resource);
    }

    /// <summary>
    /// Creates a new project resource.
    /// </summary>
    /// <returns>Returns an HTTP 201 status code with a link to the created project resource if successful, or an appropriate error status code with details if creation fails.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResourceCommandOnSuccessResponse), StatusCodes.Status200OK)]
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

    /// <summary>
    /// Closes an existing project by its unique identifier.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project to close.</param>
    /// <returns>Returns an HTTP 202 status code if the project was successfully marked as closed, otherwise HTTP 400 status code</returns>
    [HttpPatch]
    [Route("{projectId:guid}/close")]
    [ProducesResponseType(typeof(ResourceCommandOnSuccessResponse),StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ResourceCommandOnErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResourceCommandOnErrorResponse),StatusCodes.Status422UnprocessableEntity)]
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