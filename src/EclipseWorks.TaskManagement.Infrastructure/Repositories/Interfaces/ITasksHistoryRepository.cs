using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface ITasksHistoryRepository :
    IGetById<TaskHistory>,
    ICreate<TaskHistory>;