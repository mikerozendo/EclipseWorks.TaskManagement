﻿using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using EclipseWorks.TaskManagement.Models;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

public sealed class CreateProjectCommandHandler(IProjectsRepository repository)
    : IRequestHandler<CreateProjectRequest, IResourceCommandResponse>
{
    public async Task<IResourceCommandResponse> Handle(CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        await repository.CreateAsync(new Project
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            TaskIds = []
        });

        return new ResourceCommandOnSuccessResponse()
        {
            ResourceId = Guid.NewGuid()
        };
    }
}