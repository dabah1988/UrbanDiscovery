using ContactsManager.Core.Entities;

namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new country
    /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }
        public Country ToCountry()
        {
            return new Country()
            {
                CountryId = Guid.NewGuid(),
                CountryName = CountryName
            };
        }
    }
}
