using ContactsManagerWebAPI.Core.Identity;
using ContactsManagerWebAPI.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactsManagerWebAPI.Infrastructure.DatabaseContext
{
    public class ApplicationApiDbContext: IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public ApplicationApiDbContext(DbContextOptions<ApplicationApiDbContext> options):base(options) 
        {
            
        }
        public ApplicationApiDbContext()
        {

        }

        // Surcharger OnConfiguring pour le mode design-time
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Ajouter la configuration de la chaîne de connexion ici (mode design-time)
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CitiesDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
        public virtual DbSet<CityModel>? Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var citiesJson = GetCountriesFromJsonFile("cities.json");
         // citiesJson?.ForEach(myCity => modelBuilder.Entity<CityModel>().HasData(myCity));

        }
        public List<CityModel>? GetCountriesFromJsonFile(string fileName)
        {
            var countryJson = System.IO.File.ReadAllText(fileName);
            List<CityModel>? countries = System.Text.Json.JsonSerializer.Deserialize<List<CityModel>>(countryJson);
            return countries;
        }

    }
}
