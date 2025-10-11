using MediatR;
using Microservice.Shared;

namespace Catalog.API.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name):IRequest<ServiceResult<CreateCategoryResponse>>;

    // Yukarıda tek seferde yazılabiliyor
    //public record X { 
    //    public string Name { get; init; } 

    //    public X(string name)
    //    {
    //        Name = name;
    //    }
    //}
}
