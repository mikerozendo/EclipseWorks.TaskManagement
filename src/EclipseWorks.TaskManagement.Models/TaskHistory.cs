using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EclipseWorks.TaskManagement.Models;

public sealed class TaskHistory : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; }
    
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid ModifiedTaskId { get; set; }
    
    public required Guid ModifierId { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required ProjectTask TaskLastState { get; set; }
}