using System.Net;
using AutoMapper;
using Catalog.API.Repositories;
using MediatR;
using Microservice.Shared;
using Microservice.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Features.Categories.GetById
{
    public class GetCategoryByIdQuery : IRequest<ServiceResult<CategoryDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler(AppDbContext _context, IMapper _mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await _context.Categories.FindAsync(request.Id, cancellationToken);

            if (hasCategory is null)
            {
                return ServiceResult<CategoryDto>.Error(new ProblemDetails { Detail = "Category Not Found" }, HttpStatusCode.NotFound);
            }

            var mappedCategory = _mapper.Map<CategoryDto>(hasCategory);

            return ServiceResult<CategoryDto>.SuccessAsOk(mappedCategory);
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCategoryByIdQuery { Id = id });

                return result.ToGenericResult();
            });

            return group;
        }
    }
}