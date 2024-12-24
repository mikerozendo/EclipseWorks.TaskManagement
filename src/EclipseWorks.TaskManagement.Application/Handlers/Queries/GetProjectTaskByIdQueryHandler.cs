using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Queries;

public sealed class GetProjectTaskByIdQueryHandler(ITasksRepository tasksRepository)
    : IRequestHandler<GetProjectTaskByIdRequest, ResourceQueryResponse>
{
    public async Task<ResourceQueryResponse> Handle(GetProjectTaskByIdRequest request,
        CancellationToken cancellationToken) =>
        new()
        {
            Resource = await tasksRepository.GetByIdAsync(request.ResourceId)
        };
}