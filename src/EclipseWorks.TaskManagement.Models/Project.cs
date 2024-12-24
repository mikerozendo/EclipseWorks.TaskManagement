using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EclipseWorks.TaskManagement.Models;

public sealed class Project : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; }

    [BsonRepresentation(BsonType.String)] 
    public List<Guid> TaskIds { get; init; } = [];

    [BsonIgnore] 
    public List<ProjectTask> Tasks { get; init; } = [];

    public required DateTime CreatedAt { get; set; }
    public required ProjectStatus ProjectStatus { get; set; }
}