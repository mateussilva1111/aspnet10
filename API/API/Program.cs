using API.Configuration;
using API.Repositories;
using API.Repositories.Implementations;
using API.Services;
using API.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.AddSeriLogLogging();

// Add services to the container.

builder.Services.AddControllers().AddContentNegotiationConfig();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddRouteConfig();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddScoped<IPersonServices, PersonServices>();
builder.Services.AddScoped<IBooksServices, BooksServices>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerSpecification();
app.UseScalarConfiguration();

app.Run();
