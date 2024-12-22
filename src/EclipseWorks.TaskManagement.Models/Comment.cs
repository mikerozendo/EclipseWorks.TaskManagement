using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EclipseWorks.TaskManagement.Models;

public sealed class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid UserId { get; set; }
    public required string Text { get; set; }
    public required DateTime CreatedAt { get; set; }
}