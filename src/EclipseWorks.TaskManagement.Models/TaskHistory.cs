namespace EclipseWorks.TaskManagement.Models;

public sealed class TaskHistory : IEntity
{
    public required Guid Id { get; set; }
    public required Guid ModifierId { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required Guid ModifiedTaskId { get; set; }
    public required ProjectTask TaskLastState { get; set; }
}