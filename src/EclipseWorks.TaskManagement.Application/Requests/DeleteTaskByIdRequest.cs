using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class DeleteTaskByIdRequest : IRequest<IResourceCommandResponse>
{
    public Guid TaskId { get; set; }
}