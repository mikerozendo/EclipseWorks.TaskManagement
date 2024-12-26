namespace EclipseWorks.TaskManagement.Application.Responses;

public sealed class GetAnalyticsForPastDaysResponse
{
    public Guid UserId { get; set; }
    public int ClosedTasks { get; set; }
    public decimal TaskCompletionRate { get; set; }
}