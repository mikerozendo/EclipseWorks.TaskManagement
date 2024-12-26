using System.Net;
using EclipseWorks.TaskManagement.Application.Requests;
using EclipseWorks.TaskManagement.Application.Responses;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;
using EclipseWorks.TaskManagement.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Handlers.Queries;

public sealed class GetAnalyticsForPastDaysHandler(ITasksRepository tasksRepository)
    : IRequestHandler<GetAnalyticsForPastDaysRequest, IResourceResponse>
{
    public async Task<IResourceResponse> Handle(GetAnalyticsForPastDaysRequest request,
        CancellationToken cancellationToken)
    {
        var closedInTheLastDays = (await tasksRepository.GetClosedTasksByPeriodAsync(request.StartDate)).ToList();

        var closedTasksCount = closedInTheLastDays.Count;

        var response = closedInTheLastDays
            .GroupBy(x => x.UserId)
            .Select(closedTask => new GetAnalyticsForPastDaysResponse()
            {
                ClosedTasks = closedTask.Count(),
                UserId = closedTask.Key,
                TaskCompletionRate = Math.Round((decimal)closedTask.Count() / closedTasksCount * 100, 2)
            }).ToList();

        if (response.Count == 0)
        {
            return new ResourceQueryResponse(false, HttpStatusCode.NotFound);
        }

        return new ResourceQueryResponse(true, HttpStatusCode.OK, response);
    }
}