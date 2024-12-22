using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EclipseWorks.TaskManagement.Models;

public sealed class ProjectTask
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required TaskStatus Status { get; set; }
    public required TaskPriority Priority { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime DueDate { get; set; }
    public List<Comment> Comments { get; set; } = [];
}