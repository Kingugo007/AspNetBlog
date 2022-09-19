using System;
using System.ComponentModel.DataAnnotations;

namespace Medik.Domain.Model
{
    public class Post
    {
        [Key]
        public string Id { get; set; } 
        [Required]
        public string Title { get; set; }        
        [Required]
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
