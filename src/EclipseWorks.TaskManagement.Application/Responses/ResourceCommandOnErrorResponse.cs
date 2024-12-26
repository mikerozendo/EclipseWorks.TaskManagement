using System.Net;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceCommandOnErrorResponse(HttpStatusCode httpStatusCode, string details)
    : IResourceCommandResponse
{
    public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
    public object? Resource { get; set; }
    public string? Details { get; set; } = details;
    public bool Success { get; set; } = false;
}