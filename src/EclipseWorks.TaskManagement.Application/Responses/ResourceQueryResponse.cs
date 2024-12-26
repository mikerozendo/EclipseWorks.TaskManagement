using System.Net;
using EclipseWorks.TaskManagement.Application.Exceptions;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceQueryResponse(bool success, HttpStatusCode httpStatusCode, object? resource = null)
    : IResourceResponse
{
    public object? Resource { get; set; } = resource;
    public bool Success { get; set; } = success;
    public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
}