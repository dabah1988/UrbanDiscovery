using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManagerWebAPI.Core.DTO
{
    public  class RegisterDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        public string PersonName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Remote(action: "IsEmailAlreadyExists", controller: "Account", ErrorMessage = "Email is already used")]
        public string? Email { get; set; } = string.Empty;
        [RegularExpression("^[0-9 ]*$", ErrorMessage = "Invalid phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public string? Password { get; set; } 

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage ="The two password Shoud Match")]
        public string? ConfirmPassword { get; set; }
    }
}
