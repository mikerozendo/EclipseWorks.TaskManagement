using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface IGetById<T> where T : IEntity
{
    Task<T?> GetByIdAsync(Guid id);
}