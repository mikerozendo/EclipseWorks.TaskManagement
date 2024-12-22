using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface IProjectsRepository
{
    Task UpdateAsync(Project record);
    Task<Project?> GetByIdAsync(Guid id);
    Task InsertAsync(Project record);
}