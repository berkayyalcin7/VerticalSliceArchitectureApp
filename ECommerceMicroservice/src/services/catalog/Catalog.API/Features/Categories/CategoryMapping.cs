using AutoMapper;
using Catalog.API.Features.Categories;

public class CategoryMapping:Profile
{
    public CategoryMapping()
    {
        CreateMap<CategoryDto,Category>().ReverseMap();
    }
}