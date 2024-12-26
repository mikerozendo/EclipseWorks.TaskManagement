using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class CreateProjectRequest : IRequest<IResourceCommandResponse>
{
    public Guid Id { get; set; }
}