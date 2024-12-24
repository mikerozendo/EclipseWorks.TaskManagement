namespace EclipseWorks.TaskManagement.Application.Requests;

public abstract class GetResourceByIdRequest(Guid resourceId)
{
    public Guid ResourceId { get; set; } = resourceId;
}