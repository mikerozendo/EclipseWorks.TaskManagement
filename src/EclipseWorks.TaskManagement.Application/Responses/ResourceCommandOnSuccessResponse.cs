namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class ResourceCommandOnSuccessResponse : IResourceCommandResponse
{
    public Guid? ResourceId { get; set; }
    public bool Success { get; set; } = true;
}