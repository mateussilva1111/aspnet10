using API.Configuration;
using Testcontainers.MsSql;

namespace aspnet10Test.IntegrationTests.Tools
{
    public class SqlServerFixture : IAsyncLifetime
    {

        public MsSqlContainer Container { get; }

        public string ConnectionString => Container.GetConnectionString();

        public SqlServerFixture()
        {
            Container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("@Admin123")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await Container.StartAsync();
            EvolveConfig.ExecuteMigrations(ConnectionString);
        }

        public async Task DisposeAsync()
        {
            await Container.DisposeAsync();
        }
    }
}
