using ContactsManagerWebAPI.Infrastructure.DatabaseContext;
using ContactsManagerWebAPI.Core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ContactsManagerWebAPI.Controllers.V1
{
    //    [Route("api/[controller]")]
    //    [ApiController]
    /// <summary>
    /// Version 1 de l'API
    /// </summary>
    [ApiVersion("1.0")]
    //[Authorize]
    public class City : CustomControllerBase
    {
        private readonly ApplicationApiDbContext? _dbContext;
        public City(ApplicationApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// ✅ Get list of Towns
        /// </summary>
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<CityModel>>> GetCities()
        {
            if (_dbContext == null || _dbContext.Cities == null)
                return NotFound("Database is not accessible");
            var results = await _dbContext.Cities.ToListAsync();
            return (await _dbContext.Cities.ToListAsync()).OrderBy(c => c.CityName).ToList();
        }

        /// <summary>
        /// ✅ Get Town by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CityModel>> GetCity(Guid id)
        {
            if (_dbContext == null || _dbContext.Cities == null)
                return NotFound("Database is not accessible");
            var city = await _dbContext.Cities.FindAsync(id);
            if (city == null)
            {
                return Problem(detail: "Invalid cityId", statusCode: (int)HttpStatusCode.BadRequest, title: "City search");
            }
            return city;
        }


        /// <summary>
        /// ✅ Add new Town
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CityModel>> PostCity(CityModel city)
        {
            if (_dbContext == null || _dbContext.Cities == null) return NotFound("Database is not accessible");
            city.CityId = Guid.NewGuid(); // Génération automatique de l'ID
            _dbContext.Cities.Add(city);
            await _dbContext.SaveChangesAsync();
            // Retourner une réponse 201 Created avec l'URL de la nouvelle ville
            return CreatedAtAction(nameof(GetCity), new { id = city.CityId }, city); ;
        }

        /// <summary>
        /// ✅ Uodate Town
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(Guid id, CityModel city)
        {
            if (_dbContext == null || _dbContext.Cities == null) return NotFound("Database is not accessible");
            if (id != city.CityId)
            {
                return BadRequest();
            }
            _dbContext.Entry(city).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// ✅ Delete Town by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            if (_dbContext == null || _dbContext.Cities == null) return NotFound("Database is not accessible");
            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == id);
            if (city == null)
            {
                return NotFound();
            }

            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// ✅ Vérifie si une ville existe en base de données
        /// </summary>
        private bool CityExists(Guid id)
        {
            if (_dbContext == null || _dbContext.Cities == null)
                throw new ArgumentNullException("Database is not accessible ");
            return _dbContext.Cities.Any(e => e.CityId == id);
        }
    }
}
