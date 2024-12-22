using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task InsertAsync(T record);
}