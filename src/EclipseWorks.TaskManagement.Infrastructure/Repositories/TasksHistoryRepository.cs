using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public sealed class TasksHistoryRepository(EnvironmentConfiguration environmentConfiguration)
    : Repository<TaskHistory>(
        environmentConfiguration.MongoDbConfiguration.ConnectionString,
        environmentConfiguration.MongoDbConfiguration.DataBaseName,
        environmentConfiguration.MongoDbConfiguration.Collections.TasksHistory), ITasksHistoryRepository
{
    public async Task<TaskHistory> GetByIdAsync(Guid id)
        => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task CreateAsync(TaskHistory record)
        => await Collection.InsertOneAsync(record);
}