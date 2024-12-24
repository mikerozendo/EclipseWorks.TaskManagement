using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CreateTaskCommentHandler(
    ITasksRepository tasksRepository,
    ITasksHistoryRepository tasksHistoryRepository)
    : IRequestHandler<CreateTaskCommentRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CreateTaskCommentRequest request,
        CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);
        if (task is null)
        {
            return new ResourceCommandOnErrorResponse(
                HttpStatusCode.UnprocessableEntity,
                "Attempt to create a comment for a task that does not exist."
            );
        }

        await tasksHistoryRepository.CreateAsync(new TaskHistory
        {
            Id = Guid.NewGuid(),
            ModifiedTaskId = task.Id,
            ModifierId = Guid.NewGuid(), //todo: user-id
            ModifiedAt = DateTime.UtcNow,
            TaskLastState = task
        });

        task.Comments.Add(new Comment()
        {
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid() //todo: user-id
        });

        await tasksRepository.UpdateAsync(task);
        
        return new ResourceCommandOnSuccessResponse()
        {
            ResourceId = task.Id
        };
    }
}