using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface IUpdate<in T> where T : IEntity
{
    Task UpdateAsync(T entity);
}