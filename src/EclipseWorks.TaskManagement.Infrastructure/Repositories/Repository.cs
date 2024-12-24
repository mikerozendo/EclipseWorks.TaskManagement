using EclipseWorks.TaskManagement.Models;
using MongoDB.Driver;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public class Repository<T> where T : IEntity
{
    protected readonly IMongoCollection<T> Collection;

    protected Repository(string connectionString, string dataBaseName, string collectionName)
    {
        var mongoClient = new MongoClient(connectionString);
        var database = mongoClient.GetDatabase(dataBaseName);
        Collection = database.GetCollection<T>(collectionName);
    }
}