using Microsoft.OpenApi;

namespace API.Configuration
{
    public static class SwaggerConfig
    {
        private static readonly string AppName = "API asp.net 10";
        private static readonly string AppDescription = $"Aplicação desenvolvida no curso {AppName}";

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppName,
                    Version = "v1",
                    Description = AppDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Mateus José",
                        Email = "mateusjose44@hotmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                options.CustomSchemaIds(type => type.FullName);
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerSpecification(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                options.RoutePrefix = "swagger-ui"; // Define a rota raiz para a interface do Swagger
                options.DocumentTitle = AppName;

            });
            return app;
        }   
    }
}

