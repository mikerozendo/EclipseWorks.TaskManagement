using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public sealed class ProjectsRepository(EnvironmentConfiguration environmentConfiguration)
    : Repository<Project>(
        environmentConfiguration.MongoDbConfiguration.ConnectionString,
        environmentConfiguration.MongoDbConfiguration.DataBaseName,
        environmentConfiguration.MongoDbConfiguration.Collections.Projects), IProjectsRepository
{
    public async Task UpdateAsync(Project record)
    {
        var filter = MongoDB.Driver.Builders<Project>.Filter.Eq("Id", record.Id);
        await Collection.ReplaceOneAsync(filter, record);
    }
    
    public async Task<Project> GetByIdAsync(Guid id)
    {
        return await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Project record)
    {
        await Collection.InsertOneAsync(record);
    }
}