using aspnet10Test.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace aspnet10Test.IntegrationTests
{
    public class SwaggerIntegrationTest : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;

        public SwaggerIntegrationTest(SqlServerFixture fixture) 
        {
            var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
            _httpClient = factory.CreateClient(
                new WebApplicationFactoryClientOptions 
                {
                    BaseAddress = new Uri("http://localhost")
                }
                );
        }

        [Fact]
        public async Task GetSwaggerUi_ReturnsSuccess()
        {
            //Arrange
            var response = await _httpClient.GetAsync("/swagger/v1/swagger.json");
            
            //Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Com espaçamento padrão do JSON
            content.Should().NotBeNull();
            content.Should().Contain("\"openapi\": \"3.0.4\"");
            content.Should().Contain("\"title\": \"API asp.net 10\"");
            content.Should().Contain("/api/books");
        }
    }
}
