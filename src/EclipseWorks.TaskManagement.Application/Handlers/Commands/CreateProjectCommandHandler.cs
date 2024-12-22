using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CreateProjectCommandHandler(IProjectsRepository repository)
    : IRequestHandler<CreateProjectRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await repository.InsertAsync(new Project()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Tasks = []
            });
            
            return new ResourceCommandOnSuccessResponse()
            {
                ResourceId = Guid.NewGuid()
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}