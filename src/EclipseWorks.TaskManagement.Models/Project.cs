namespace EclipseWorks.TaskManagement.Models;

public sealed class Project : IEntity
{
    public required Guid Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public IEnumerable<ProjectTask> Tasks { get; set; } = [];
}