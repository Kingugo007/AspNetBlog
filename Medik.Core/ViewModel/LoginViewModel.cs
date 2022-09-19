using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Medik.Core.ViewModel
{
    public class LoginViewModel
    {
       
        [Required(ErrorMessage = "Please enter a valid email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your sceret key")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
