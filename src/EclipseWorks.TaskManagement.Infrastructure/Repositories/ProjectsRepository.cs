using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public sealed class ProjectsRepository(EnvironmentConfiguration environmentConfiguration)
    : Repository<Project>(
        environmentConfiguration.MongoDbConfiguration.ConnectionString,
        environmentConfiguration.MongoDbConfiguration.DataBaseName,
        environmentConfiguration.MongoDbConfiguration.Collections.Projects), IRepository<Project>;