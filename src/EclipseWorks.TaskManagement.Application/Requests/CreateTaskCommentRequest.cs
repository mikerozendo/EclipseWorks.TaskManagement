using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class CreateTaskCommentRequest : IRequest<IResourceCommandResponse>
{
    public Guid TaskId { get; set; }
    public string Text { get; set; }
}