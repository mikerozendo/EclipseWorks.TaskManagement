namespace EclipseWorks.TaskManagement.Application.Responses;

public interface IResourceCommandResponse : IResourceResponse
{
    public string? Details { get; set; }
}