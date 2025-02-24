using ContactsManager.Core.Entities;
namespace ContactsManager.Core.DTO
{
    public class CountryResponse
    {
        /// <summary>
        /// DTO used for return for most of countries service method
        /// </summary>
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country()
            {
                CountryId = CountryId,
                CountryName = CountryName
            };
        }
    }

    public static class CountryResponseExtension
    {

        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName
            };
        }






    }
}
