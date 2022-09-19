using System.ComponentModel.DataAnnotations;

namespace Medik.Core.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter valid name")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter valid name")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter a valid email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a sceret key")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Comfirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

    }
}
