using API.Configuration;
using API.Services;
using API.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddScoped<IPersonServices, PersonServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
