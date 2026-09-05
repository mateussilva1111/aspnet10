using API.Data.Dto;


namespace API.Services
{
    public interface IPersonServices
    {

        Task<IEnumerable<PersonDTO>> GetAllAsync();
        Task<PersonDTO> GetByIdAsync(long id);
        Task<PersonDTO> CreateAsync(PersonDTO person);
        Task<PersonDTO> UpdateAsync(PersonDTO person);
        Task<bool> DeleteAsync(long id);
        Task<PersonDTO> DisableAsync(long id);
    }
}
