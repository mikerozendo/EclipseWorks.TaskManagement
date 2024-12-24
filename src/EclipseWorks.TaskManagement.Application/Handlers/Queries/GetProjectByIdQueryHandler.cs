using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Queries;

public sealed class GetProjectByIdQueryHandler(IProjectsRepository projectsRepository, ITasksRepository tasksRepository)
    : IRequestHandler<GetProjectByIdQueryRequest, ResourceQueryResponse>
{
    public async Task<ResourceQueryResponse> Handle(GetProjectByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var filteredProject = await projectsRepository.GetByIdAsync(request.ResourceId);
        if (filteredProject is null)
        {
            return new ResourceQueryResponse();
        }


        var tasksRelatedToThisProject = await tasksRepository.GetByProjectIdAsync(request.ResourceId);
        filteredProject.Tasks.AddRange(tasksRelatedToThisProject);

        return new ResourceQueryResponse
        {
            Resource = filteredProject
        };
    }
}