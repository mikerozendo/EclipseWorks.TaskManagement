using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EclipseWorks.TaskManagement.Models;

public sealed class ProjectTask : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public required Guid ProjectId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required ProjectTaskStatus Status { get; set; }
    public required TaskPriority Priority { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime DueDate { get; set; }
    public List<Comment> Comments { get; set; } = [];
    public DateTime? ClosedAt { get; set; }
}