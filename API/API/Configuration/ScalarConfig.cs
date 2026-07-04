using Scalar.AspNetCore;

namespace API.Configuration
{
    public static class ScalarConfig
    {
        private static readonly string AppName = "API asp.net 10";
        private static readonly string AppDescription = $"Aplicação desenvolvida no curso {AppName}";
        public static WebApplication UseScalarConfiguration(this WebApplication app)
        {
            app.MapScalarApiReference("/scalar", options =>
            {
                options
                .WithTitle(AppName)
                .WithOpenApiRoutePattern("/swagger/v1/swagger.json");
            });
            return app;
        }
    }
}
