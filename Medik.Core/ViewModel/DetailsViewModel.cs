using Medik.Domain.Model;
using System.Collections.Generic;

namespace Medik.Core.ViewModel
{
    public class DetailsViewModel
    {
        public Post Post { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public string Identity { get; set; }
        public StatViewModel Statistic {get; set; }
    }
}
