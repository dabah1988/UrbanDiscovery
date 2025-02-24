
using Microsoft.EntityFrameworkCore;
using ContactsManager.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ContactsManager.Core.Domain.IdentityEntities;
namespace ContactsManager.Infrastructure.MyDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public virtual DbSet<Person>? Persons { get; set; } 
        public virtual  DbSet<Country>? Countries { get; set; }  


        // Constructeur sans paramètre pour éviter l'erreur en mode design-time
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Surcharger OnConfiguring pour le mode design-time
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Ajouter la configuration de la chaîne de connexion ici (mode design-time)
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PersonsDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            // Relation entre Person et Country (Clé étrangère)
            modelBuilder.Entity<Person>()
                .HasOne<Country>(p => p.Country) // Une Person a un Country
                .WithMany() // Un Country peut avoir plusieurs Person
                .HasForeignKey(p => p.CountryId) // La clé étrangère est CountryId dans Person
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade (si tu veux que la suppression d'un pays supprime les personnes associées)


            // Add of constraint column for name, Email, PhoneNumber
            modelBuilder.Entity<Person>().Property(p => p.Name)
               .HasColumnType("varchar(100)");

            modelBuilder.Entity<Person>().Property(p => p.Email)
             .HasColumnType("varchar(30)");

            modelBuilder.Entity<Person>().Property(p => p.PhoneNumber)
             .HasColumnType("varchar(20)");

            modelBuilder.Entity<Person>().Property(p => p.Address)
             .HasColumnType("varchar(50)")
              .HasDefaultValue("04 rue su vieux marché aux vins 67 000 strasbourg")
             ;

            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            {
                // Ne pas charger les fichiers JSON en design-time
                return;
            }

            //List<Country>? countries = GetCountriesFromJsonFile("countries.json");
            //List<Person>? persons = GetPeopleFromJsonFile("persons.json");
            //countries?.ForEach(country => modelBuilder.Entity<Country>().HasData(country));
            //persons?.ForEach(person => modelBuilder.Entity<Person>().HasData(person));

        }

        //public List<Person>? sp_GetAllPersons()
        //{
        //    return Persons?.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        //}

        // public  int sp_InsertPerson(Person person)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //         new SqlParameter("@Id",person.Id),
        //          new SqlParameter("@Name",person.Name),
        //           new SqlParameter("@Email",person.Email),
        //          new SqlParameter("@PhoneNumber",person.PhoneNumber),
        //          new SqlParameter("@DateOfBirth",person.DateOfBirth),
        //           new SqlParameter("@CountryId",person.CountryId),
        //           new SqlParameter("@Address",person.Address)
        //    };
        //    return Database.ExecuteSqlRaw("EXECUTE[dbo].[sp_InsertPerson] @Id," +
        //        " @Name, @Email, @PhoneNumber, @DateOfBirth,@CountryId , Address", parameters);
        //}
        public List<Country>? GetCountriesFromJsonFile(string fileName)
        {
            var countryJson = System.IO.File.ReadAllText(fileName);
            List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countryJson);
            return countries;
        }


        public List<Person>? GetPeopleFromJsonFile(string fileName)
        {
            var personJson = System.IO.File.ReadAllText(fileName);
            List<Person>? persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personJson);
            return persons;
        }

    }
}
