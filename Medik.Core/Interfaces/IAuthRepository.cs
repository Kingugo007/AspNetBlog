using Medik.Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medik.Infrastructure
{
    public interface IAuthRepository
    {
        IConfiguration _configuration { get; }
        string connectionString { get; set; }
        Task<bool> AddUser(User user);
        Task<List<User>> GeAllUsers();
    }
}