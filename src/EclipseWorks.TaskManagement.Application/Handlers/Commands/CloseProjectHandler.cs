using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CloseProjectHandler(
    ITasksRepository tasksRepository,
    IProjectsRepository projectsRepository)
    : IRequestHandler<CloseProjectByIdRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CloseProjectByIdRequest byIdRequest, CancellationToken cancellationToken)
    {
        var project = await projectsRepository.GetByIdAsync(byIdRequest.ProjectId);
        if (project is null)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.UnprocessableEntity,
                "Attempt to update a project that doesn't exist"
            );
        }

        var tasks = await tasksRepository.GetByProjectIdAsync(byIdRequest.ProjectId);

        if (!tasks.All(x => x.Status is ProjectTaskStatus.Done))
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.BadRequest,
                "There are tasks that are not done in this project, please, finish them before updating project to close"
            );
        }

        project.ProjectStatus = ProjectStatus.Closed;
        await projectsRepository.UpdateAsync(project);

        return new ResourceCommandOnSuccessResponse()
        {
            Resource = project.Id
        };
    }
}