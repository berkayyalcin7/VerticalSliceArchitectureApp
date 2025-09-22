public static class CategoryEndpointExtension
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/categories").CreateCategoryGrouItemEndpoint().RequireAuthorization();
    }
}