using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class UpdateTaskDetailsRequest 
    : IRequest<IResourceCommandResponse>
{
    public Guid ProjectId { get; set; }
    public Guid TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus TaskStatus { get; set; }
}