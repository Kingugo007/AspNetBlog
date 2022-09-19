using Medik.Core.Interfaces;
using Medik.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medik.Infrastructure.ProfileRepository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly string CommentFile = "Comments.json";
        private readonly string _userFile = "Users.json";
        private readonly IJsonOperations _dbContext;
        public List<Comment> Comments { get; set; }
        public ProfileRepository(IJsonOperations dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Comment> GetProfile(int Id)
        { 
            try
            {
                var comments = await _dbContext.ReadJson<Comment>(CommentFile);
                return comments.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Comment>> GetAllProfile()
        {
            try
            {
                return await _dbContext.ReadJson<Comment>(CommentFile);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> GetUserProfile(string email)
        {
            try 
            {
                var users = await _dbContext.ReadJson<User>(_userFile);
                return users.FirstOrDefault(x => x.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
