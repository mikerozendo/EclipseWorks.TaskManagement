using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/tasks")]
public sealed class TasksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("{taskId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid taskId)
    {
        var response = await mediator.Send(new GetProjectTaskByIdRequest(taskId));

        if (!response.Success)
            return NotFound();

        return Ok(response.Resource);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProjectTaskRequest createProjectTaskRequest)
    {
        var response = await mediator.Send(createProjectTaskRequest);

        if (response.Success)
        {
            return CreatedAtAction(
                nameof(GetById),
                new { taskId = ((ResourceCommandOnSuccessResponse)response).Resource }, response
            );
        }

        return Problem(response.Details, statusCode: (int)response.HttpStatusCode);
    }

    [HttpPatch]
    [Route("{taskId:guid}/status")]
    public async Task<IActionResult> UpdateTaskStatus([FromRoute] Guid taskId, [FromBody] ProjectTaskStatus taskStatus)
    {
        var response = await mediator.Send(new UpdateTaskStatusRequest
        {
            TaskId = taskId,
            ProjectTaskStatus = taskStatus
        });

        if (!response.Success)
            return Problem(response.Details, statusCode: (int)response.HttpStatusCode);

        return Accepted();
    }

    [HttpPost]
    [Route("{taskId:guid}/comments")]
    public async Task<IActionResult> PostComment([FromRoute] Guid taskId, [FromBody] string text)
    {
        var response = await mediator.Send(new CreateTaskCommentRequest
        {
            TaskId = taskId,
            Text = text
        });

        if (!response.Success)
            return Problem(response.Details, statusCode: (int)response.HttpStatusCode);

        return Accepted();
    }

    [HttpDelete]
    [Route("{taskId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid taskId)
    {
        var response = await mediator.Send(new DeleteTaskByIdRequest
        {
            TaskId = taskId,
        });

        if (!response.Success)
            return Problem(response.Details, statusCode: (int)response.HttpStatusCode);

        return NoContent();
    }
}