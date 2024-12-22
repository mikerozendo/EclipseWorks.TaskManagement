namespace EclipseWorks.TaskManagement.Models;

public sealed class ProjectHistory : IEntity
{
    public required Guid Id { get; set; }
    public required Guid ModifierId { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required Guid ProjectId { get; set; }
    public required Project ProjectLastState { get; set; }
}