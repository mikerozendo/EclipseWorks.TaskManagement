using System.Net;

namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceCommandOnSuccessResponse : ResourceCommandResponse
{
    public Guid? ResourceId { get; set; }
    public bool Success => true;
    public HttpStatusCode HttpStatusCode { get; set; }
    public string? Details { get; set; }
}