using KodaStore.Api.Data;
using Scalar.AspNetCore;
using KodaStore.Api.Files;
using KodaStore.Api.Middlewares;
using KodaStore.Api.Repositories;
using KodaStore.Api.Services;
using Microsoft.EntityFrameworkCore;
// using KodaStore.Api.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails(
    option =>
    {
        option.CustomizeProblemDetails = context =>
        {
            context.ProblemDetails.Instance ??= context.HttpContext.Request.Path.Value;

            context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        };
    }
);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// builder.Services.AddSingleton<InMemoryProductCatalog>();

// builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
// builder.Services.AddSingleton<IProductService, ProductService>();

// Repository
// builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
builder.Services.AddScoped<IProductRepository, EfProductRepository>();

// Service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageStorage, LocalProductImageStorage>();


var connectionString = builder.Configuration.GetConnectionString("ecommercedb") 
    ?? throw new InvalidOperationException("ga bisa koneksi, belum konfigurasi");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
        // .UseSeeding((context, _) => ProductSeedData.Seed(context))
        // .UseAsyncSeeding((context,_,ct) => ProductSeedData.SeedAsync(context,ct));
}); 


var app = builder.Build();
app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();

    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Koda Store API v1");
        options.RoutePrefix = "swagger";

        options.DocumentTitle = "Koda Store | API Documentation";

        options.EnableFilter();
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();