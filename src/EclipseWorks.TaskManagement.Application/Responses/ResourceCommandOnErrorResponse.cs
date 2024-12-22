using System.Net;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceCommandOnErrorResponse(HttpStatusCode httpStatusCode, string details)
    : IResourceCommandResponse
{
    public HttpStatusCode HttpStatusCode { get; private set; } = httpStatusCode;
    public string Details { get; private set; } = details;
    public bool Success { get; set; } = false;
}