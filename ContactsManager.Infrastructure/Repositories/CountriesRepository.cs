
using ContactsManager.Core.Entities;
using ContactsManager.Infrastructure.MyDbContext;
using Microsoft.EntityFrameworkCore;
using ContactsManager.Core.RepositoryContract;

namespace ContactsManager.Infrastructure.Repositories
{
    public class CountriesRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CountriesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext  ;

        }
        public async Task<Country> AddCountry(Country country)
        {
            _dbContext.Countries!.Add(country);
            await _dbContext.SaveChangesAsync();
            return country;
        }

        public async Task DeleteCountry(Country country)
        {
             _dbContext.Countries!.Remove(country);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _dbContext.Countries!.ToListAsync();
        }

        public async Task<Country?> GetCountryById(Guid countryId)
        {
            return await _dbContext.Countries!.FirstOrDefaultAsync(c => c.CountryId == countryId);
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            Country? countryFound = await _dbContext.Countries!
                .FirstOrDefaultAsync(c => c.CountryId == country.CountryId);
        if (countryFound != null)
            {
             countryFound.CountryName = country.CountryName;    
            }
           await  _dbContext.SaveChangesAsync();
            return country;

        }
    }
}
