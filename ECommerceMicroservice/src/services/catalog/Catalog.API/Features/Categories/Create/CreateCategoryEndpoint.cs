using Catalog.API.Features.Categories.Create;
using MediatR;
using Microservice.Shared.Extensions;
using Microservice.Shared.Filters;

public static class CreateCategoryEndpoint
{
    public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        
        group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);

            return result.ToGenericResult();

        }).AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();
        // Generic olduğu için Gruba veremiyoruz Endpoint Filter'i.



        return group;
    }
}