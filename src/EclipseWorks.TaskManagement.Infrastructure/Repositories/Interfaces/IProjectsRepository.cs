using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface IProjectsRepository :
    IGetById<Project>,
    ICreate<Project>,
    IUpdate<Project>;