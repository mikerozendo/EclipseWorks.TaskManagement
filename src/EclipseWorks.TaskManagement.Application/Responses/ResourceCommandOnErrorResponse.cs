using System.Net;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceCommandOnErrorResponse(HttpStatusCode httpStatusCode, string details)
    : IResourceCommandResponse
{
    public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
    public object? Resource { get; set; }
    public string? Details { get; set; } = details;
    public bool Success { get; set; } = false;
}