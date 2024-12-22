﻿using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class CreateProjectRequest : IRequest<ResourceCommandResponse>
{
    public Guid Id { get; set; }
}