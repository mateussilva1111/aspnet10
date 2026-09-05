using aspnet10Test.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace aspnet10Test.IntegrationTests
{
    [TestCaseOrderer("aspnet10Test.IntegrationTests.Tools.PriorityOrder", "aspnet10Test.IntegrationTests")]
    public class PersonCorsIntegrationTest : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;

        public PersonCorsIntegrationTest(SqlServerFixture fixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
            _httpClient = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost")
                }
                );
        }

        private void AddOriginHeader(string origin)
        {
            _httpClient.DefaultRequestHeaders.Remove("Origin");
            _httpClient.DefaultRequestHeaders.Add("Origin", origin);
        }

        [Fact(DisplayName = "GetPerson with allowed origin"), TestPriority(1)]
        public async Task GetPersonwithAllowedOrigin()
        {
            AddOriginHeader("https://erudio.com.br");
            var response = await _httpClient.GetAsync("/api/person/1");
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<API.Models.Person>();

            created.Should().NotBeNull();
            created.FirstName.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "GetPerson with disallowed origin"), TestPriority(1)]
        public async Task GetPersonwithDisallowedOrigin()
        {
            AddOriginHeader("https://disallowed.com");
            var response = await _httpClient.GetAsync("/api/person/1");
            //response.EnsureSuccessStatusCode();   

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var created = await response.Content.ReadAsStringAsync();

            created.Should().Be("Origin not allowed");
        }
    }
}
