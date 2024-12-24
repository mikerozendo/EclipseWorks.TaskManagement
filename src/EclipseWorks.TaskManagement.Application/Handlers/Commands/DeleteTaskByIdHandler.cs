using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class DeleteTaskByIdHandler(IProjectsRepository projectsRepository, ITasksRepository tasksRepository)
    : IRequestHandler<DeleteTaskByIdRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(DeleteTaskByIdRequest request,
        CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);
        if (task is null)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.BadRequest,
                "Attempt to delete a task that does not exist."
            );
        }

        var project = await projectsRepository.GetByIdAsync(task.ProjectId);
        project.TaskIds.Remove(request.TaskId);

        await tasksRepository.DeleteByIdAsync(request.TaskId);
        await projectsRepository.UpdateAsync(project);

        return new ResourceCommandOnSuccessResponse();
    }
}