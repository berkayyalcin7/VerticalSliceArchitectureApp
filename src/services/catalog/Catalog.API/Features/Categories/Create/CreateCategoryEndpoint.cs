using Catalog.API.Features.Categories.Create;
using MediatR;
using Microservice.Shared.Extensions;

public static class CreateCategoryEndpoint
{
    public static RouteGroupBuilder CreateCategoryGrouItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            return result.ToGenericResult();
        });

        return group;
    }
}