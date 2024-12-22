using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface IProjectsHistoryRepository
{
    Task InsertAsync(ProjectHistory projectHistory);
}