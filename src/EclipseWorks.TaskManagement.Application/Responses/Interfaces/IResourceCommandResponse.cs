namespace EclipseWorks.TaskManagement.Application.Responses.Interfaces;

public interface IResourceCommandResponse : IResourceResponse
{
    public string? Details { get; set; }
}