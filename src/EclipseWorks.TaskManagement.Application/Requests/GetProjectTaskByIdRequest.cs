using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class GetProjectTaskByIdRequest(Guid resourceId)
    : GetResourceByIdRequest(resourceId), IRequest<ResourceQueryResponse>;