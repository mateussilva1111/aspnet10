using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace aspnet10Test.IntegrationTests.Tools
{
    public class CustomWebApplicationFactory<TProgram>
            : WebApplicationFactory<TProgram> where TProgram : class
    {

        private readonly string _conecttioString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _conecttioString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var dict = new Dictionary<string, string>
                {
                    {
                        "MSSQLServerSQLConnection:ConnectionString",
                        _conecttioString 
                    }
                };
                config.AddInMemoryCollection(dict!);
            });

        }
    }
}
