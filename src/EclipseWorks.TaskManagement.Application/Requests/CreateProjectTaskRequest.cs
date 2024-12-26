using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class CreateProjectTaskRequest : IRequest<IResourceCommandResponse>
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ProjectTaskStatus ProjectTaskStatus { get; set; }
    public TaskPriority TaskPriority { get; set; }
    public DateTime DueDate { get; set; }
}
