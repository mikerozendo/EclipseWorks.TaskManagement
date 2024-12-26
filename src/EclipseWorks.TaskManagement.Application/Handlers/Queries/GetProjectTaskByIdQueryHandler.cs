using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Queries;

public sealed class GetProjectTaskByIdQueryHandler(ITasksRepository tasksRepository)
    : IRequestHandler<GetProjectTaskByIdRequest, ResourceQueryResponse>
{
    public async Task<ResourceQueryResponse> Handle(GetProjectTaskByIdRequest request,
        CancellationToken cancellationToken)
    {
        var resource = await tasksRepository.GetByIdAsync(request.ResourceId);
        
        if (resource is null)
            return new ResourceQueryResponse(false, HttpStatusCode.NotFound);

        return new ResourceQueryResponse(true, HttpStatusCode.OK, resource);
    }
}