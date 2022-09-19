using System;
using System.ComponentModel.DataAnnotations;

namespace Medik.Domain.Model
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required()]
        public string FirstName { get; set; }
        [Required()]
        public string LastName { get; set; }
        [Required()]
        public string Email { get; set; }
        [Required()]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
