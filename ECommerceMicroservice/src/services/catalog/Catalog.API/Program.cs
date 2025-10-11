using Catalog.API;
using Catalog.API.Features.Categories.Create;
using Catalog.API.Options;
using Catalog.API.Repositories;
using MediatR;
using Microservice.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptionsExt();

builder.Services.AddDatabaseServiceExt();

builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));

var app = builder.Build();

// Set the port to 5180 to match launchSettings.json
app.Urls.Add("http://localhost:5180");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "Catalog API v1");
        c.RoutePrefix = string.Empty; // Swagger UI'yi root'ta aç
    });
}

app.AddCategoryGroupEndpointExt();

app.MapGet("/", () => Results.Ok("Catalog API is running"));

app.Run();
