using System.Linq.Expressions;
using EclipseWorks.TaskManagement.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

    public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(T record)
    {
        await Collection.InsertOneAsync(record);
    }
}