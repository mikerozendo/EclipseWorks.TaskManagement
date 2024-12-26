using System.Net;
using EclipseWorks.TaskManagement.Application.Exceptions;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceQueryResponse(bool success, HttpStatusCode httpStatusCode, object? resource = null)
    : IResourceResponse
{
    public object? Resource { get; set; } = resource;
    public bool Success { get; set; } = success;
    public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
}