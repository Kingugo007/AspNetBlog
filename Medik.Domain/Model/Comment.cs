using System;
using System.Collections.Generic;
using System.Text;

namespace Medik.Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Profession { get; set; }
        public string Degree { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; } 
    }
}
