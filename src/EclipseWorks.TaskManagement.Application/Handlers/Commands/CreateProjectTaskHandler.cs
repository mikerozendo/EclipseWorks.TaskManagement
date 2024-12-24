using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CreateProjectTaskHandler(
     IProjectsRepository projectsRepository,
     ITasksRepository tasksRepository)
    : IRequestHandler<CreateProjectTaskRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CreateProjectTaskRequest request,
        CancellationToken cancellationToken)
    {
        var project = await projectsRepository.GetByIdAsync(request.ProjectId);
        if (project is null)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.UnprocessableEntity,
                "Attempt to create a task for a project that doesnt even exist"
            );
        }

        if (project.TaskIds.Count >= 20)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.UnprocessableEntity,
                "Attempt to create a task for a project that has more than 20 tasks"
            );
        }

        var requestedTask = new ProjectTask
        {
            Description = request.Description,
            Id = Guid.NewGuid(),
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            DueDate = request.DueDate,
            Status = request.TaskStatus,
            Priority = request.TaskPriority,
        };
        
        project.TaskIds.Add(requestedTask.Id);
        
        await projectsRepository.UpdateAsync(project);
        await tasksRepository.CreateAsync(requestedTask);

        return new ResourceCommandOnSuccessResponse()
        {
            ResourceId = requestedTask.Id,
        };
    }
}