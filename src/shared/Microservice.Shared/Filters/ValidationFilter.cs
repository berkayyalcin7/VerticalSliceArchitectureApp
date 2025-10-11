using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Shared.Filters
{
public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        
        if (validator == null)
            {
                 return await next(context);
            }

        var requestModel=context.Arguments.OfType<T>().FirstOrDefault();

        if (requestModel == null)
            {
                return await next(context);
            }

        var validationResult = await validator.ValidateAsync(requestModel);
        
        if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
        return await next(context);
    }
}
}