namespace API.Configuration
{
    public static class CorsConfig
    {
        private static string[] GetAllowedOrigins(IConfiguration configuration)
        {
            return configuration.GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? Array.Empty<string>();
        }

        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = GetAllowedOrigins(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });

                options.AddPolicy("MultipleOriginPolicy",
                    policy =>
                    {
                        policy.WithOrigins(
                            "http://localhost:3000",
                            "http://localhost:4200",
                            "https://erudio.com.br")
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });

                options.AddPolicy("DefaultOriginPolicy",
                    policy =>
                    {
                        policy.WithOrigins(origins)
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });
            });
        }

        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            var origins = GetAllowedOrigins(configuration);

            app.Use(async (context, next) =>
            {
                var origin = context.Request.Headers["Origin"].ToString();

                if (!string.IsNullOrEmpty(origin) && !origins.Contains(origin, StringComparer.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Origin not allowed");
                    return;
                }

                await next();
            });

            app.UseCors("DefaultOriginPolicy");
            return app;
        }
    }
}