using API.Data.Converter.Contract;
using API.Data.Dto;
using API.Models;

namespace API.Data.Converter.Implementation
{
    public class PersonConverter : IParser<PersonDTO, Person>, IParser<Person, PersonDTO>
    {
        public PersonDTO Parse(Person origin)
        {
            if (origin == null) return null;

            return new PersonDTO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<PersonDTO> ParseList(List<Person> originList)
        {
            if (originList == null) return null;

            return originList.Select(Parse).ToList();
        }

        public Person Parse(PersonDTO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<Person> ParseList(List<PersonDTO> originList)
        {
            if (originList == null) return null;
            return originList.Select(Parse).ToList();
        }
    }
}
  