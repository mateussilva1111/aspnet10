using API.Hypermedia;
using API.Hypermedia.Enricher;

namespace API.Configuration
{
    public static class HateoasConfig
    {
        public static IServiceCollection AddHateoasConfiguration(this IServiceCollection services)
        {
            var filterOptions = new HypermediaFilterOpttions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);
            services.AddScoped<HypermediaFilter>();

            return services;
        }

        public static void UseHateoasRoutes(this IEndpointRouteBuilder app)
        {
            app.MapControllerRoute(
                "default",
                "{controller=Values}/{action=Index}/{id?}"
            );
        }
    }
}
