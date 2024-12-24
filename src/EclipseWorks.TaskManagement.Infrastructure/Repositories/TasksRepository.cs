using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public sealed class TasksRepository(EnvironmentConfiguration environmentConfiguration)
    : Repository<ProjectTask>(
        environmentConfiguration.MongoDbConfiguration.ConnectionString,
        environmentConfiguration.MongoDbConfiguration.DataBaseName,
        environmentConfiguration.MongoDbConfiguration.Collections.Tasks), ITasksRepository
{
    public async Task<ProjectTask> GetByIdAsync(Guid id)
        => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task CreateAsync(ProjectTask record)
        => await Collection.InsertOneAsync(record);
}