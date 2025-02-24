using ContactsManagerWebAPI.Infrastructure  .DatabaseContext;
using ContactsManagerWebAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ContactsManagerWebAPI.Controllers.V2
{
 
    /// <summary>
    /// Version 2 de l'API
    /// </summary>
    [ApiVersion("2.0")]
    public class City : CustomControllerBase
    {
        private readonly ApplicationApiDbContext? _dbContext;
        public City(ApplicationApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// ✅ Get list of City name
        /// </summary>
        [HttpGet]
        [Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            if (_dbContext == null || _dbContext.Cities == null)
                return NotFound("Database is not accessible");
            return await _dbContext.Cities.Select(c => c.CityName).ToListAsync();
        }

    }
}
