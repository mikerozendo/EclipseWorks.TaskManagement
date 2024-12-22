using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetById(Guid id);
    Task Insert(T record);
}