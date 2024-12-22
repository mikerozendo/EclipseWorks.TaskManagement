using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EclipseWorks.TaskManagement.Models;

public sealed class ProjectTask : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required TaskStatus TaskStatus { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime DueDate { get; set; }
    public IEnumerable<Comment> Comments { get; set; } = [];
}