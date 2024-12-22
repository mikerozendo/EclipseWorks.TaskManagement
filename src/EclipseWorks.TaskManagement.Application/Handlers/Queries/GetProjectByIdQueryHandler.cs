using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Queries;

public sealed class GetProjectByIdQueryHandler(IProjectsRepository projectsRepository)
    : IRequestHandler<GetProjectByIdQueryRequest, ResourceQueryResponse>
{
    public async Task<ResourceQueryResponse> Handle(GetProjectByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        return new ResourceQueryResponse()
        {
            Resource = await projectsRepository.GetByIdAsync(request.ProjectId),
        };
    }
}