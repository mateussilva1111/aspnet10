using aspnet10Test.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace aspnet10Test.IntegrationTests
{
    public class ScalarIntegrationTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;

        public ScalarIntegrationTests(SqlServerFixture fixture)
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
        public async Task GetScalarUi_ReturnsSuccess()
        {
            //Arrange
            var response = await _httpClient.GetAsync("/scalar/");
            //Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            content.Should().Contain("API asp.net 10");
        } 
    }
}

