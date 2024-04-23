using BookLibrary;
using BookLibrary.Entities;
using BookLibrary.Filters;
using BookLibrary.Repositories;
using BookLibrary.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<BookLibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Base")));

builder.Services.AddScoped<IBookRepository<Book>, BookRepository<Book>>();
builder.Services.AddScoped<IUserRepository<User>, UserRepository<User>>();
builder.Services.AddScoped<JwtAuthorizationFilter>();
builder.Services.AddTransient<JwtService>();
builder.Services.AddSingleton<Configuration>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


ODataConventionModelBuilder modelBuilder = new();
modelBuilder.EntitySet<Book>("Books");

builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy().Expand().SetMaxTop(100);
    options.AddRouteComponents("odata", modelBuilder.GetEdmModel());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Book API", Version = "v1" });

    c.OperationFilter<ODataSwaggerOperationFilter>();
});

var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Book API v1");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
