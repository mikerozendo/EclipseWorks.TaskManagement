using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Models;
using MediatR;
using TaskStatus = EclipseWorks.TaskManagement.Models.TaskStatus;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class CreateProjectTaskRequest : IRequest<IResourceCommandResponse>
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus TaskStatus { get; set; }
    public TaskPriority TaskPriority { get; set; }
    public DateTime DueDate { get; set; }
}
