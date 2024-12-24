using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class GetProjectByIdQueryRequest(Guid projectId)
    : GetResourceByIdRequest(projectId), IRequest<ResourceQueryResponse>;