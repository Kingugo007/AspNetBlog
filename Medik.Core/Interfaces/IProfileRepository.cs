using Medik.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medik.Infrastructure.ProfileRepository
{
    public interface IProfileRepository
    {
        List<Comment> Comments { get; set; }
        Task<List<Comment>> GetAllProfile();
        Task<Comment> GetProfile(int Id);
        Task<User> GetUserProfile(string email);
    }
}