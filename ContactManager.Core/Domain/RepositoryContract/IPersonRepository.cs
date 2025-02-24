using ContactsManager.Core.Entities;

namespace ContactsManager.Core.RepositoryContract
{
    /// <summary>
    /// repository is the link between personDataseBase and PersonService
    /// </summary>
    public  interface IPersonRepository
    {
        public Task<Person> AddPerson(Person person);
        public Task<List<Person>> GetAllPersons();

        public Task<Person> GetPersonByPersonId(Guid personId);

        public Task<Person?> UpdatePerson(Person person);

        public Task<bool> DeletePerson(Person person);
    }
}
