using System.Net;

namespace EclipseWorks.TaskManagement.Application.Responses.Interfaces;

public interface IResourceResponse
{
    public bool Success { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public object? Resource { get; set; }
}