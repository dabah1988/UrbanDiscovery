using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ContactsManagerWebAPI.Core.Models
{
    /// <summary>
    /// Class City 
    /// </summary>
    public class CityModel
    {
        /// <summary>
        /// Id of City
        /// </summary>
        [Key]
        public Guid CityId { get; set; }
        [Required(ErrorMessage = "CityName is required")]

        /// <summary>
        /// Name
        /// </summary>
        public string? CityName { get; set; }

        /// <summary>
        /// Population
        /// </summary>
        public int CityPopulation { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        public double CityArea { get; set; }
    }
}
