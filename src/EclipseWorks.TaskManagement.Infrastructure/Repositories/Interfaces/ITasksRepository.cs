﻿using EclipseWorks.TaskManagement.Models;

namespace EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;

public interface ITasksRepository :
    IGetById<ProjectTask>,
    ICreate<ProjectTask>,
    IUpdate<ProjectTask>;