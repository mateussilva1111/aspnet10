using Microsoft.OpenApi;

namespace API.Configuration
{
    public static class OpenApiConfig
    {
        private static readonly string AppName = "API asp.net 10";
        private static readonly string AppDescription = $"Aplicação desenvolvida no curso {AppName}";

        public static IServiceCollection AddOpenApiConfig(this IServiceCollection services)
        {
            
            services.AddSingleton(new OpenApiInfo
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
            return services;
        }
    }
}
