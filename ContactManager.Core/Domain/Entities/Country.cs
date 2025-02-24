using System.ComponentModel.DataAnnotations;


namespace ContactsManager.Core.Entities
{
    /// <summary>
    /// Domain model for country details
    /// </summary>
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }
        [MaxLength(100)]
        public string? CountryName { get; set; }
        //public virtual ICollection<Person>? persons { get; set; } = new HashSet<Person>(); 
       
    }

}
