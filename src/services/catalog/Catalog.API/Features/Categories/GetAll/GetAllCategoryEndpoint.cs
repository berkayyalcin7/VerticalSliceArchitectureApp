using System.Linq;
using AutoMapper;
using Catalog.API.Repositories;
using MediatR;
using Microservice.Shared;
using Microservice.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
public record CategoryDto(Guid Id,string Name);
public class GetAllCategoryQuery : IRequest<ServiceResult<List<CategoryDto>>>
{

}
public class GetAllCategoryQueryHandler(AppDbContext _context,IMapper _mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
{

    public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync(cancellationToken);

        // AUTOMAPPER İLE MAPLAMA YAPILIR.
        var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

        return ServiceResult<List<CategoryDto>>.SuccessAsOk(mappedCategories);

    }
}


public static class GetAllCategoryEndpoint
{
    public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllCategoryQuery());

            return result.ToGenericResult();
        });

        return group;
    }
}
