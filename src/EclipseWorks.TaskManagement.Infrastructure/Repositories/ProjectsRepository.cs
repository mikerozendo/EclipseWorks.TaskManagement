using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public sealed class ProjectsRepository(EnvironmentConfiguration environmentConfiguration)
    : Repository<Project>(
        environmentConfiguration.MongoDbConfiguration.ConnectionString,
        environmentConfiguration.MongoDbConfiguration.DataBaseName,
        environmentConfiguration.MongoDbConfiguration.Collections.Projects), IRepository<Project>, IProjectsRepository
{
    public async Task UpdateAsync(Project record)
    {
        var filter = MongoDB.Driver.Builders<Project>.Filter.Eq("Id", record.Id);
        await Collection.ReplaceOneAsync(filter, record);
    }
}