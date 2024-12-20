namespace EclipseWorks.TaskManagement.Models;

public sealed class Comment
{
    public required string Text { get; set; }
    public required Guid UserId { get; set; }
    public required DateTime CreatedAt { get; set; }
}