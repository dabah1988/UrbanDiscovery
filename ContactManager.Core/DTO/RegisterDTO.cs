using ContactsManager.Core.enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContactsManager.Core.DTO
{
    public  class RegisterDTO
    {
        [Required(ErrorMessage ="Name is mandatory")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Email is mandatory")]
        [Remote(action: "IsEmailAlreadyRegistered",controller:"Account", ErrorMessage ="Email already registered in database")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is mandatory")]
        [RegularExpression("^[0-9 ]*$", ErrorMessage ="Phone should contain only numbers")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is mandatory")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirm password does not match with Password ")]
        public string? ConfirmPassword { get; set; }

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;  

    }
}
