using Medik.Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Medik.Infrastructure
{
    public interface ICommentRepository
    {
        IConfiguration _configuration { get; }
        string connectionString { get; set; }
        List<Comment> GetAllComment();
        Comment GetComment(int id);
        User GetUserProfile(string id);
    }
}