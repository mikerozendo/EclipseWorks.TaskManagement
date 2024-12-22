using System.Net;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceCommandOnErrorResponse : ResourceCommandResponse
{
    public bool Success => false;
    public HttpStatusCode HttpStatusCode { get; set; }
    public string? Details { get; set; }
}