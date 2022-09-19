using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Medik.Core.ViewModel
{
    public class PostViewModel
    {
        [Required(ErrorMessage = "Please enter a valid title")]
        public string Title { get; set; }      
        [Required(ErrorMessage = "Please write content")]
        public string Content { get; set; }
        public IFormFile Photo { get; set; }
        public string Identity { get; set; } 
    }
}
