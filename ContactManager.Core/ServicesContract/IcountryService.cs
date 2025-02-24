using ContactsManager.Core.DTO;
using ContactsManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.ServicesContract
{
    /// <summary>
    /// Business logic for manipulate country entity
    /// </summary>
    public  interface IcountryService
    {
        /// <summary>
        /// Add country to list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to de added </param>
        /// <returns>CountryResponse after adding </returns>
        Task<CountryResponse?> AddCountry(CountryAddRequest countryAddRequest);
        Task<List<CountryResponse>?> GetAllCountries();
        Task<CountryResponse?> GetCountryResponseById(Guid countryId);


        public Task<bool> RemoveCountry(Country country);
    }
}
