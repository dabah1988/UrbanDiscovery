using ContactsManager.Core.DTO;
using ContactsManager.Core.Entities;

namespace ContactsManager.Core.ServicesContract
{
    public interface IPersonService
    {
        Task<PersonResponse?> AddPerson(PersonAddRequest personAddRequest);
        Task<List<PersonResponse>?> GetAllPersons();
        Task<PersonResponse?> GetPersonById(Guid personId);
        public Task<bool> RemovePerson(Person person);
        Task<PersonResponse?> UpdatePerson(Person person);
    }
}