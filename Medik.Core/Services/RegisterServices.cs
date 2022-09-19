using Medik.Core.Interfaces;
using Medik.Core.Utilities;
using Medik.Core.ViewModel;
using Medik.Domain.Model;
using Medik.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Medik.Core.Services
{
    public class RegisterServices : IAuthServices
    {
        private readonly IAuthRepository _auth;
        public RegisterServices(IAuthRepository auth)
        {
            _auth = auth;
        }
        public async Task<RegisterViewModel> Register(RegisterViewModel newUser)
        {
            if (await IsExist(newUser.Email))
            {
                return null;
            }
            else
            {
                User user = new User()
                {
                   FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Password = newUser.Password.ComputeSha256Hash(),
                    CreatedDate = DateTime.Now,
                };
                var added = await _auth.AddUser(user);
                if (added)
                {
                    return newUser;
                }
                else
                {
                    return null;
                }
            }      
        }
        public async Task<User> Login(LoginViewModel userInfo)
        {
            var allUsers = await _auth.GeAllUsers();
            foreach (var user in allUsers)
            {
                if (user.Email.Equals(userInfo.Email) && user.Password.Equals(userInfo.Password.ComputeSha256Hash()))
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<bool> IsExist(string email)
        {
            var allUsers = await _auth.GeAllUsers();
            return allUsers.Exists(x => x.Email.Equals(email));
        }
    }
}
