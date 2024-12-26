using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class UpdateTaskStatusHandler(
    ITasksRepository tasksRepository,
    ITasksHistoryRepository tasksHistoryRepository)
    : IRequestHandler<UpdateTaskStatusRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(UpdateTaskStatusRequest request,
        CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);
        if (task is null)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.UnprocessableEntity,
                "Attempt to update a task that doesn't exist"
            );
        }

        await tasksHistoryRepository.CreateAsync(new TaskHistory()
        {
            ModifiedAt = DateTime.UtcNow,
            ModifiedTaskId = task.Id,
            ModifierId = Guid.NewGuid(), //todo: user_id
            Id = Guid.NewGuid(),
            TaskLastState = task,
        });

        task.Status = request.ProjectTaskStatus;
        await tasksRepository.UpdateAsync(task);

        return new ResourceCommandOnSuccessResponse()
        {
            Resource = task.Id
        };
    }
}