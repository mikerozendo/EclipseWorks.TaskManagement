using System.Net;

namespace EclipseWorks.TaskManagement.Application.Responses;

public class ResourceCommandResponse
{
    public bool Success { get; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public string? Details { get; set; }
}