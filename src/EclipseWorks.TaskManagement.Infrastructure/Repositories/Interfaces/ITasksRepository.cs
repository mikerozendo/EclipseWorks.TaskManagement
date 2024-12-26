using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface ITasksRepository :
    IGetById<ProjectTask>,
    ICreate<ProjectTask>,
    IUpdate<ProjectTask>
{
    Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId);
    Task DeleteByIdAsync(Guid taskId);
    Task<IEnumerable<ProjectTask>> GetClosedTasksByPeriodAsync(DateTime startPeriod);
}