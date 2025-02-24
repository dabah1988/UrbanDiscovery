using Microsoft.EntityFrameworkCore;
using ContactsManager.Core.RepositoryContract;
using ContactsManager.Infrastructure.MyDbContext;
using ContactsManager.Core.Entities;

namespace ContactsManager.Infrastructure.Repositories
{
    public class PersonsRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public PersonsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async  Task<Person> AddPerson(Person person)
        {
            _dbContext.Persons!.Add(person);
            await _dbContext.SaveChangesAsync();   
            return person;  
        }

        public async Task<bool> DeletePerson(Person person)
        {
            try
            {
                var personFound = await _dbContext.Persons!.FirstOrDefaultAsync(p => p.Id == person.Id);
                if (personFound == null) return false;
                _dbContext.Persons!.Remove(personFound);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return false;
        }

        public async Task<List<Person>> GetAllPersons()
        {
            return await _dbContext.Persons!.Include(p => p.Country)!.ToListAsync();
        }

        public async Task<Person> GetPersonByPersonId(Guid personId)
        {
            if (_dbContext == null) throw new ObjectDisposedException(nameof(ApplicationDbContext));
            Person? person =  await _dbContext.Persons!.Include(p => p.Country).FirstOrDefaultAsync(p => p.Id == personId);
            if(person == null) return null; 
            return person;
        }

        public async Task<Person?> UpdatePerson(Person person)
        {
            try
            {
                if (_dbContext == null) throw new ObjectDisposedException(nameof(ApplicationDbContext));

                Person? personFound = await GetPersonByPersonId(person.Id);
                if (personFound != null)
                {
                    personFound.Address = person.Address;
                    personFound.PhoneNumber = person.PhoneNumber;
                    personFound.DateOfBirth = person.DateOfBirth;
                    personFound.Name = person.Name;
                    personFound.Country = person.Country;
                    personFound.CountryId = person.CountryId;
                    await _dbContext.SaveChangesAsync();
                }
                
               
                return personFound;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
