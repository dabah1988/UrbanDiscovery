using ContactsManager.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class PersonResponse
    {
        [Key] // Clé primaire
        public Guid Id { get; set; }

        [Required] // Champ obligatoire
        [MaxLength(100)] // Longueur maximale
        public string? Name { get; set; }

        [EmailAddress] // Validation d'email
        public string? Email { get; set; }

        [Phone] // Validation de numéro de téléphone
        public string? PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Clé étrangère vers une autre entité (ex: City)
        public Guid CountryId { get; set; }

        public Country? Country { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PhoneNumber = PhoneNumber,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Name = Name,
                CountryId = CountryId,
                Id = Id,
                Country = Country
            };
        }

    }

    public static class PersonExteion
    {
        public static PersonResponse ToPersonResonse(this Person person)
        {
            return new PersonResponse()
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                DateOfBirth = person.DateOfBirth,
                CountryId = person.CountryId,
                Country = person.Country

            };
        }
    }


}
