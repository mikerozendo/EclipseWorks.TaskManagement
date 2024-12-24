using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class UpdateTaskStatusRequest : IRequest<IResourceCommandResponse>
{
    public Guid TaskId { get; set; }
    public ProjectTaskStatus ProjectTaskStatus { get; set; }
}