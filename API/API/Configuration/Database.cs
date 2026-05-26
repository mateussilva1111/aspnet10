using Microsoft.EntityFrameworkCore;

namespace API.Configuration
{
    public static class Database
    {
        public static IServiceCollection AddDatabaseConfiguration
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["MSSQLServerSQLConnection:ConnectionString"];
            if(string.IsNullOrEmpty(connectionString) ) {
                throw new InvalidOperationException("Connection string 'MSSQLServerSQLConnection' not found.");
            }
            services.AddDbContext<Models.Context.MSSQLContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
    }
}
