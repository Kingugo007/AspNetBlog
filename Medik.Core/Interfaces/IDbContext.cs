using Medik.Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Medik.Infrastructure
{
    public interface IDbContext
    {
        IConfiguration _configuration { get; }
        string connectionString { get; set; }
        List<Post> GetAllPost();
        Post GetPost(string id);
    }
}