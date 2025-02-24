using ContactsManager.Core.DTO;
using ContactsManager.Core.Entities;

using ContactsManager.Core.RepositoryContract;
using ContactsManager.Core.ServicesContract;
 

namespace ContactsManager.Core.Services
{
    public class CountryService : IcountryService
    {
        List<Country>? countries {  get; set; }
        private readonly ICountryRepository _countriesRepository;
        public CountryService(ICountryRepository countriesRepository)
        {
            countries = new List<Country>();  
            _countriesRepository = countriesRepository; 
        }

        /// <summary>
        /// Add country add request to databse
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public  async Task<CountryResponse?> AddCountry(CountryAddRequest countryAddRequest)
        {
            //if (_countriesRepository == null  || _countriesRepository.Countries == null ) 
            //    throw new ArgumentNullException( nameof(_countriesRepository), "BDContext is null");

            if(countryAddRequest == null)   throw new ArgumentNullException(nameof(countryAddRequest));
            if (countryAddRequest.CountryName == null) throw new ArgumentException(nameof(countryAddRequest));
            if( (await _countriesRepository.GetAllCountries()).Any( c => c.CountryName == countryAddRequest.CountryName))
                throw new ArgumentException($" {nameof(countryAddRequest)} is duplicated");
            CountryResponse? countryResponse = countryAddRequest.ToCountry().ToCountryResponse();
            await _countriesRepository.AddCountry(countryAddRequest.ToCountry());
            return countryResponse;
        }

        /// <summary>
        /// Retrieve all countries from database
        /// </summary>
        /// <returns>List of countryResponse</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<List<CountryResponse>?> GetAllCountries()
        {
           List<CountryResponse> results = (await _countriesRepository.GetAllCountries()).ToList().Select( c => c.ToCountryResponse()).ToList();
           return results;
        }

        /// <summary>
        /// Get country by Id
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<CountryResponse?> GetCountryResponseById(Guid countryId)
        {
            var country = await _countriesRepository.GetCountryById(countryId);
            if (country == null)
                throw new KeyNotFoundException($"Country with ID {countryId} not found.");
            return country.ToCountryResponse();
        }

        /// <summary>
        /// Delete country
        /// </summary>
        /// <param name="country"></param>
        public async Task<bool> RemoveCountry(Country country)
        {

            Country? countrySearched = await _countriesRepository.GetCountryById(country.CountryId);
            if(countrySearched != null) (await _countriesRepository.GetAllCountries()).Remove(countrySearched);
            return false;   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Country"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<CountryResponse?> UpdateCountry(Country country)
        {         
           Country?  CountryMatched = await  _countriesRepository.GetCountryById(country.CountryId);
            if(CountryMatched != null )
            {
                 CountryMatched.CountryName = country.CountryName;
                 CountryMatched.CountryId = country.CountryId;
                await _countriesRepository.UpdateCountry(CountryMatched);
                return CountryMatched?.ToCountryResponse();
            }
            return null;
        }


    }
}
