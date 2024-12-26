using EclipseWorks.TaskManagement.Application.Exceptions;
using EclipseWorks.TaskManagement.Application.Responses;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Behavior;

public sealed class ResourceResponseValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TResponse : IResourceResponse
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        var integerStatusCode = (int)response.HttpStatusCode;
        var isSuccessStatusCode = integerStatusCode is >= 200 and <= 299;

        if (!isSuccessStatusCode && !(response.Success || response.Resource is not null))
        {
            throw new InvalidResponseConfigurationException();
        }

        return response;
    }
}