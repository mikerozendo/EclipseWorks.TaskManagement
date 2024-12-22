namespace EclipseWorks.TaskManagement.Models;

public sealed class TasksHistory : IEntity
{
    public Guid Id { get; set; }
    public required Guid ModifierId { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required IEnumerable<Task> TasksOldState { get; set; } = [];
}