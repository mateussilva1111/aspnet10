using API.Data.Converter.Implementation;
using API.Data.Dto;
using API.Models;

namespace aspnet10Test
{
    public  class PersonCoverterTest
    {

        private readonly PersonConverter _personCoverter;

        public PersonCoverterTest()
        {
            _personCoverter = new PersonConverter();
        }

        [Fact]
        public void ParseTeste()
        {
            //arrage
            var personDto = new PersonDTO
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main"
            };
            var peson = new Person 
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main"
            };

            //act
            var person = _personCoverter.Parse(personDto);

            //assert
            Assert.NotNull(person);
            Assert.Equal(1, person.Id);
            Assert.Equal("John", person.FirstName);
            Assert.Equal("Doe", person.LastName);
            Assert.Equal("123 Main", person.Address);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
