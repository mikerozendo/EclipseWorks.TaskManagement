using System.Net;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceCommandOnSuccessResponse : IResourceCommandResponse
{
    // public Guid? ResourceId { get; set; }
    public bool Success { get; set; } = true;
    public HttpStatusCode HttpStatusCode { get; set; }
    public object? Resource { get; set; }
    public string? Details { get; set; }
}