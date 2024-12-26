using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class GetAnalyticsForPastDaysRequest : IRequest<ResourceQueryResponse>
{
    public DateTime StartDate => DateTime.UtcNow.AddDays(-30);
}