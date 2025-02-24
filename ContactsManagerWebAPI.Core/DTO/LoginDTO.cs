using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManagerWebAPI.Core.DTO
{
    public  class LoginDTO
    {
        [EmailAddress]
        [Required(ErrorMessage ="Email Cannot be empty")]
        public string Email {  get; set; } = string.Empty;
        [Required(ErrorMessage ="Password cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
