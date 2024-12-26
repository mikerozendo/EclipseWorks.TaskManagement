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
    /// <summary>
    /// Retrieves a task by its unique identifier.
    /// </summary>
    /// <param name="taskId">The unique identifier of the task to retrieve.</param>
    /// <returns>An IActionResult containing the task details if found; otherwise, a NotFound result.</returns>
    [HttpGet]
    [Route("{taskId:guid}")]
    [ProducesResponseType(typeof(ResourceQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResourceQueryResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid taskId)
    {
        var response = await mediator.Send(new GetProjectTaskByIdRequest(taskId));

        if (!response.Success)
            return NotFound();

        return Ok(response.Resource);
    }

    /// <summary>
    /// Creates a new task for a specific project.
    /// </summary>
    /// <param name="createProjectTaskRequest">The request object containing details of the task to be created, such as project ID, title, description, task status, priority, and due date.</param>
    /// <returns>An IActionResult containing the details of the created task on success, or a problem response with error details on failure.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResourceCommandOnSuccessResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResourceCommandOnErrorResponse), StatusCodes.Status422UnprocessableEntity)]
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

    /// <summary>
    /// Updates the status of a specific task.
    /// </summary>
    /// <param name="taskId">The unique identifier of the task whose status is being updated.</param>
    /// <param name="taskStatus">The new status to assign to the task.</param>
    /// <returns>An IActionResult indicating the outcome of the operation. Returns status code 200 for success or 422 for failure.</returns>
    [HttpPatch]
    [Route("{taskId:guid}/status")]
    [ProducesResponseType(typeof(ResourceCommandOnSuccessResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ResourceCommandOnErrorResponse), StatusCodes.Status422UnprocessableEntity)]
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

    /// <summary>
    /// Adds a new comment to a specific task.
    /// </summary>
    /// <param name="taskId">The unique identifier of the task to which the comment will be added.</param>
    /// <param name="text">The text content of the comment to be added.</param>
    /// <returns>An IActionResult indicating the result of the operation. Returns a 202 Accepted on success or a 422 Unprocessable Entity in case of failure.</returns>
    [HttpPost]
    [Route("{taskId:guid}/comments")]
    [ProducesResponseType(typeof(ResourceCommandOnSuccessResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ResourceCommandOnErrorResponse), StatusCodes.Status422UnprocessableEntity)]
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

    /// <summary>
    /// Deletes a task by its unique identifier.
    /// </summary>
    /// <param name="taskId">The unique identifier of the task to be deleted.</param>
    /// <returns>An IActionResult indicating the outcome of the delete operation.
    /// Returns NoContent if the deletion is successful or a Problem result with details if an error occurs.</returns>
    [HttpDelete]
    [Route("{taskId:guid}")]
    [ProducesResponseType(typeof(ResourceCommandOnSuccessResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResourceCommandOnErrorResponse), StatusCodes.Status400BadRequest)]
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