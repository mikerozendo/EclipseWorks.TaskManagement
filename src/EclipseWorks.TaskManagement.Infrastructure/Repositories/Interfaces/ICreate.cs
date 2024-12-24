using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface ICreate<in T>  where T : IEntity
{
    Task CreateAsync(T record);
}