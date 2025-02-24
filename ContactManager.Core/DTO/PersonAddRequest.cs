using ContactsManager.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class PersonAddRequest
    {

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

    }

    public static class PersonExtension
    {
        public static Person ToPerson(this PersonAddRequest personAddRequest)
        {
            return new Person()
            {
                Id = Guid.NewGuid(),
                Name = personAddRequest.Name,
                Email = personAddRequest.Email,
                PhoneNumber = personAddRequest.PhoneNumber,
                DateOfBirth = personAddRequest.DateOfBirth,
                CountryId = personAddRequest.CountryId,
                Country = personAddRequest.Country,
            };
        }

    }
}
