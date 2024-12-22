using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class GetProjectByIdQueryRequest(Guid projectId) 
    : IRequest<ResourceQueryResponse>
{
    public Guid ProjectId { get; set; } = projectId;
}