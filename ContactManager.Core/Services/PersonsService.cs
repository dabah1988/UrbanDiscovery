using ContactsManager.Core.DTO;
using ContactsManager.Core.Entities;
using ContactsManager.Core.RepositoryContract;
using ContactsManager.Core.ServicesContract;
 

namespace ContactsManager.Core.Services
{
    public class PersonsService : IPersonService
    {
         private readonly IPersonRepository? _personRepository;
        private readonly Task<List<Person>> _people;  
        public PersonsService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            
        }
        /// <summary>
        /// Add Person
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<PersonResponse?> AddPerson(PersonAddRequest personAddRequest)
        {
            if ( personAddRequest == null ) throw new ArgumentNullException($"{nameof(personAddRequest)} is null ");
            if (_personRepository == null) throw new ArgumentNullException($"{nameof(_personRepository)} is null ");
            if (string.IsNullOrEmpty(personAddRequest.Name)) throw new ArgumentNullException($"{nameof(personAddRequest.Name)} is null or empty");
            if ( (await _personRepository.GetAllPersons()).Any(p => p.Email == personAddRequest.Email)) throw new ArgumentException("Email is duplicated");
            Person person = personAddRequest.ToPerson();
            await _personRepository. AddPerson(person);
            return person.ToPersonResonse();
        }

        /// <summary>
        ///  Get All persons
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<List<PersonResponse>?> GetAllPersons()
        {
            return (await _personRepository!.GetAllPersons()).ToList().Select(p => p.ToPersonResonse()).ToList();

        }

        /// <summary>
        /// Get Person Person Id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">return PersonResponse</exception>
        public async Task<PersonResponse?> GetPersonById(Guid personId)
        {

            Person? personFound = await _personRepository!.GetPersonByPersonId(personId);
            if(personFound != null)  return personFound.ToPersonResonse();  
             return null;
        }

        /// <summary>
        /// Update all data of persons
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<PersonResponse>? UpdatePerson(Person person)
        {
            Person? personMatch = await _personRepository!.GetPersonByPersonId(person.Id);
            if (personMatch != null)
            {
                personMatch.Name = person.Name;
                personMatch.PhoneNumber = person.PhoneNumber;
                personMatch.Email = person.Email;   
                personMatch.DateOfBirth = person.DateOfBirth;
                personMatch.CountryId = person.CountryId;
                await _personRepository.UpdatePerson(personMatch);
            }
      
            return person.ToPersonResonse();
        }

        /// <summary>
        /// Delete persons
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> RemovePerson(Person person)
        {
            Person? personFound = await _personRepository!.GetPersonByPersonId(person.Id);
            if (personFound != null)
            {
                await _personRepository.DeletePerson(personFound);
                return true;
            }
            return false;
        }

         
    }
}
