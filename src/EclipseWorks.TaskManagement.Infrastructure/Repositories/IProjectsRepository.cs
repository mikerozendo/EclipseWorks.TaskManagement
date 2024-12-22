using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public interface IProjectsRepository
{
    Task UpdateAsync(Project record);
}