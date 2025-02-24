using ContactsManager.Core.Entities;

namespace ContactsManager.Core.RepositoryContract
{
    /// <summary>
    /// Data access logic for manipulate country entity
    /// </summary>
    public interface  ICountryRepository
    {
        /// <summary>
        /// Add a country object to data store
        /// </summary>
        /// <param name="country"></param>
        /// <returns>Object  country created </returns>
        Task<Country> AddCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        /// <summary>
        /// Delete country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        Task DeleteCountry(Country country);
        /// <summary>
        /// Return Country By Id 
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns>Country based on Id supplied </returns>
        Task<Country?> GetCountryById(Guid countryId);

        /// <summary>
        /// Return ALL countries
        /// </summary>
        /// <returns></returns>
        Task<List<Country>> GetAllCountries();

    }
}
