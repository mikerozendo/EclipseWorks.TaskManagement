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
    public async Task<ProjectTask?> GetByIdAsync(Guid id)
        => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task CreateAsync(ProjectTask record)
        => await Collection.InsertOneAsync(record);

    public async Task UpdateAsync(ProjectTask entity)
        => await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

    public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId)
        => await Collection
            .AsQueryable()
            .Where(task => task.ProjectId == projectId)
            .ToListAsync();

    public async Task DeleteByIdAsync(Guid taskId)
        => await Collection.DeleteOneAsync(task => task.Id == taskId);

    public async Task<IEnumerable<ProjectTask>> GetClosedTasksByPeriodAsync(DateTime startPeriod)
        => await Collection
            .AsQueryable()
            .Where(x =>
                x.Status >= ProjectTaskStatus.Done &&
                x.ClosedAt.HasValue &&
                x.ClosedAt.Value.Date >= startPeriod.Date
            )
            .ToListAsync();
}