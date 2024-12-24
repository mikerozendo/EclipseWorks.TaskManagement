﻿using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
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

        if (response.Resource is null)
            return NotFound();

        return Ok(response.Resource);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProjectTaskRequest createProjectTaskRequest)
    {
        var response = await mediator.Send(createProjectTaskRequest);
    
        if (response.Success) 
            return Created();
    
        var error = (ResourceCommandOnErrorResponse)response;
        return Problem(error.Details, statusCode: (int)error.HttpStatusCode);
    }
}