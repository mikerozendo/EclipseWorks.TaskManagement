using System.Net;

namespace EclipseWorks.TaskManagement.Application.Responses;

public interface IResourceCommandResponse
{
    public bool Success { get; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public string? Details { get; set; }
}