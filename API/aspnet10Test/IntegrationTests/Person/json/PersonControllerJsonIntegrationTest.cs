using API.Data.Dto;
using aspnet10Test.IntegrationTests.Tools;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace aspnet10Test.IntegrationTests.Person.json
{
    [TestCaseOrderer("aspnet10Test.IntegrationTests.Tools.PriorityOrder", "aspnet10Test.IntegrationTests")]
    public class PersonControllerJsonIntegrationTest : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private PersonDTO _person;

        public PersonControllerJsonIntegrationTest(SqlServerFixture fixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
            _httpClient = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost")
                }
                );
        }


        [Fact(DisplayName = "GetPerson with allowed origin")]
        [TestPriority(7)]
        public async Task GetPersonwithAllowedOrigin()
        {
            var response = await _httpClient.GetAsync("/api/person/1");
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<API.Models.Person>();

            created.Should().NotBeNull();
            created.FirstName.Should().NotBeNull();
        }


        [Fact(DisplayName = "teste de integração criação")]
        [TestPriority(1)]
        public async Task CreatePersonWithAllowedOrigin()
        {
            var request = new PersonDTO
            {               
                Gender = "M",
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/person/", request);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<PersonDTO>();
            created.Should().NotBeNull();

            created.Should().NotBeNull();
            created!.Id.Should().BeGreaterThan(0);
            created!.FirstName.Should().Be("John");
            created.LastName.Should().Be("Doe");
            created.Address.Should().Be("123 Main St");

            _person = created;

        }

        [Fact(DisplayName = "teste de integração atualização")]
        [TestPriority(2)]
        public async Task UpdatePersonWithAllowedOrigin()
        {
            var request = new PersonDTO
            {
                Id = 52,
                Gender = "M",
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                Enabled = true
            };

            var response = await _httpClient.PutAsJsonAsync($"/api/person/", request);          
            response.EnsureSuccessStatusCode();

            var updated = await response.Content.ReadFromJsonAsync<PersonDTO>();
            updated.Should().NotBeNull();

            updated.Should().NotBeNull();
            updated!.FirstName.Should().Be("John");
            updated.LastName.Should().Be("Doe");
            updated.Address.Should().Be("123 Main St");

            _person = updated;
        }

        [Fact(DisplayName = "teste de integração findbyid")]
        [TestPriority(3)]
        public async Task FindById()
        {
            var response = await _httpClient.GetAsync("/api/person/1");
            response.EnsureSuccessStatusCode();
            var person = await response.Content.ReadFromJsonAsync<PersonDTO>();
            person.Should().NotBeNull();
            person.Should().NotBeNull();
            person!.FirstName.Should().Be("John");
            person.LastName.Should().Be("Doe");
            person.Address.Should().Be("123 Main St");

            _person = person;
        }   

        [Fact(DisplayName = "desabilitar")]
        [TestPriority(4)]
        public async Task DisablePersonWithAllowedOrigin()
        {
           var response = await _httpClient.PatchAsync("/api/person/1", null);
            response.EnsureSuccessStatusCode();

            //assert
            var disabled = await response.Content.ReadFromJsonAsync<PersonDTO>();
            disabled.Should().NotBeNull();
            disabled!.Enabled.Should().Be(false);

            _person = disabled;
        }

        [Fact(DisplayName = "teste de integração exclusão")]
        [TestPriority(5)]
        public async Task DeletePersonWithAllowedOrigin()
        {
            var response = await _httpClient.DeleteAsync($"/api/person/52");
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Obter todos")]
        [TestPriority(6)]
        public async Task GetAllPersons()
        {
            var response = await _httpClient.GetAsync("/api/person");
            response.EnsureSuccessStatusCode();

            var persons = await response.Content.ReadFromJsonAsync<List<PersonDTO>>();
            persons.Should().NotBeNull();
        }
    }
}
