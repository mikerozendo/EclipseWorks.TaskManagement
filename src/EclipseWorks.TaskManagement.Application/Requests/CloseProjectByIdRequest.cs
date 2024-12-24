﻿using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class CloseProjectByIdRequest : IRequest<IResourceCommandResponse>
{
    public Guid ProjectId { get; set; }
}