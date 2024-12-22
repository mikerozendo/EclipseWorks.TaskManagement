using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CreateProjectTaskHandler(
    IRepository<Project> projectRepository,
    IRepository<ProjectHistory> tasksHistoryRepository,
    IProjectsRepository projectsRepository)
    : IRequestHandler<CreateProjectTaskRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CreateProjectTaskRequest request,
        CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId);
        if (project is null)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.UnprocessableEntity,
                "Attempt to create a task for a project that doesnt even exist"
            );
        }

        if (project.Tasks.Count >= 20)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.UnprocessableEntity,
                "Attempt to create a task for a project that has more than 20 tasks"
            );
        }

        var projectHistory = new ProjectHistory()
        {
            Id = Guid.NewGuid(),
            ModifiedAt = DateTime.UtcNow,
            ModifierId = Guid.NewGuid(),
            ProjectLastState = project,
            ProjectId = project.Id
        };

        project.Tasks.Add(new ProjectTask()
        {
            Description = request.Description,
            Id = Guid.NewGuid(),
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            DueDate = request.DueDate,
            Status = request.TaskStatus,
            Priority = request.TaskPriority,
        });
        
        await projectsRepository.UpdateAsync(project);
        await tasksHistoryRepository.InsertAsync(projectHistory);

        return new ResourceCommandOnSuccessResponse()
        {
            ResourceId = projectHistory.Id,
        };
    }
}