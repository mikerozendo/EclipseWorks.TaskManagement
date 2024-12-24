using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Commands;

// public class UpdateTaskDetailsHandler(ITasksHistoryRepository tasksHistoryRepository)
//     : IRequestHandler<UpdateTaskDetailsRequest, IResourceCommandResponse>
// {
//     public async Task<IResourceCommandResponse> Handle(UpdateTaskDetailsRequest request,
//         CancellationToken cancellationToken)
//     {
//         var filteredProject = await tasksHistoryRepository.GetByIdAsync(request.ProjectId);
//         if (filteredProject is null)
//         {
//             return new ResourceCommandOnErrorResponse(
//                 HttpStatusCode.UnprocessableEntity,
//                 "Attempt to create a task for a project that doesnt even exist"
//             );
//         }
//         
//         var task = 
//         
//     }
// }