using EclipseWorks.TaskManagement.Application.Exceptions;
using EclipseWorks.TaskManagement.Application.Responses.Interfaces;
using MediatR;

namespace EclipseWorks.TaskManagement.Application.Behaviors;

public sealed class ResourceResponseValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TResponse : IResourceResponse
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        //made to throws exception avoiding invalid response configurations by another devs
        var response = await next();

        var integerStatusCode = (int)response.HttpStatusCode;
        var isSuccessHttpStatusCode = integerStatusCode is >= 200 and <= 299;

        if (isSuccessHttpStatusCode &&
            response.Success &&
            response.Resource is null)
            throw new InvalidResponseConfigurationException();

        return response;
    }
}