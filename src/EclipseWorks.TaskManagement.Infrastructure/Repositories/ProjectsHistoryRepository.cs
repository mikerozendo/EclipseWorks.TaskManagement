using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public sealed class ProjectsHistoryRepository(EnvironmentConfiguration environmentConfiguration)
    : Repository<ProjectHistory>(
        environmentConfiguration.MongoDbConfiguration.ConnectionString,
        environmentConfiguration.MongoDbConfiguration.DataBaseName,
        environmentConfiguration.MongoDbConfiguration.Collections.ProjectsHistory), IProjectsHistoryRepository
{
    public async Task InsertAsync(ProjectHistory projectHistory) 
        => await Collection.InsertOneAsync(projectHistory);
}