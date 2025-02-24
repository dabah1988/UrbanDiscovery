using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.DTO
{
    public  class LoginDTO
    {

        [Required(ErrorMessage = "Email is mandatory")]
        [EmailAddress(ErrorMessage ="Email i not valid")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
