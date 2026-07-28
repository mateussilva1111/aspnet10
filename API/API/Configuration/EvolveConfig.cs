using EvolveDb;
using Microsoft.Data.SqlClient;
using Serilog;

namespace API.Configuration
{
    public static class EvolveConfig
    {
        public static IServiceCollection AddEvolveConfiguration(this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                var connectionString = configuration["MSSQLServerSQLConnection:ConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'MSSQLServerSQLConnection' not found.");
                }

                try
                {
                    ExecuteMigrations(connectionString);                    
                }
                catch (Exception e)
                {
                    Log.Error("Database migration failed: {ErrorMessage}", e.Message);
                    throw;
                }
            }

            return services;
        }

        public static void ExecuteMigrations(string connectionString)
        {
            using var evolveConnection = new SqlConnection(connectionString);
            var evolve = new Evolve(
                evolveConnection,
                msg => Log.Information(msg))
            {
                Locations = new[] { "DB/Migrations", "DB/Datasets" },
                IsEraseDisabled = true,
            };
            evolve.Migrate();
        }
    }
}
