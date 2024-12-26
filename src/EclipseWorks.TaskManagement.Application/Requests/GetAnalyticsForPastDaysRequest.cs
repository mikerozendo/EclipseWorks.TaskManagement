using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Requests;

public sealed class GetAnalyticsForPastDaysRequest(int days) : IRequest<ResourceQueryResponse>
{
    public int Days { get; set; } = days;

    public DateTime StartDate => DateTime.UtcNow.AddDays(days * -1);
}