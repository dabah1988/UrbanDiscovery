using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.Entities
{

    public class Person
    {
        [Key] // Clé primaire
              //public int Id { get; set; }
        public Guid Id { get; set; }

        [Required] // Champ obligatoire
        [MaxLength(100)] // Longueur maximale
        public string? Name { get; set; }

        [EmailAddress] // Validation d'email
        [MaxLength (100)]
        public string? Email { get; set; }

        [Phone] // Validation de numéro de téléphone
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Clé étrangère vers une autre entité (ex: City)
        [ForeignKey(nameof(CountryId))]
        public Guid CountryId { get; set; }

        [MaxLength(50)]
        public string? Address { get; set; }

        public Country? Country { get; set; }

    }
}
