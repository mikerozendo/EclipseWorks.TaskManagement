using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.TaskManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/tasks")]
public sealed class TasksController(IMediator mediator) : ControllerBase
{
    // [HttpPut]
    // [Route("/tasks")]
    // public async Task<IActionResult> Post([FromBody] CreateProjectTaskRequest createProjectTaskRequest)
    // {
    //     var response = await mediator.Send(createProjectTaskRequest);
    //
    //     if (response.Success) 
    //         return Created();
    //
    //     var error = (ResourceCommandOnErrorResponse)response;
    //     return Problem(error.Details, statusCode: (int)error.HttpStatusCode);
    // }
    
    [HttpPost]
    [Route("/tasks")]
    public async Task<IActionResult> Post([FromBody] CreateProjectTaskRequest createProjectTaskRequest)
    {
        var response = await mediator.Send(createProjectTaskRequest);
    
        if (response.Success) 
            return Created();
    
        var error = (ResourceCommandOnErrorResponse)response;
        return Problem(error.Details, statusCode: (int)error.HttpStatusCode);
    }
}