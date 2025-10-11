using Catalog.API.Repositories;
using MassTransit;
using MediatR;
using Microservice.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext _context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _context.Categories.AnyAsync(x => x.Name == request.Name);

            if (existingCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Error(new ProblemDetails { Detail="Category Name Already Exists"},HttpStatusCode.BadRequest);
            }

            var category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid()
            };
            // sıralı GUID oluşturma
            await _context.AddAsync(category,cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"Empty");
        }
    }
}
