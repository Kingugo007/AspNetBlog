using Medik.Core.Interfaces;
using Medik.Core.Utilities;
using Medik.Core.ViewModel;
using Medik.Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Medik.Infrastructure.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IJsonOperations _dbContext;
        private readonly string _jsonfile = "Users.json";
        public AccountRepository(IJsonOperations jsonOperations)
        {
            _dbContext = jsonOperations;
        }
        public async Task<Response> Register(RegisterViewModel userInfo)
        {
            try
            {
                bool status;
                User newUser = new User()
                {
                    CreatedDate = DateTime.Now,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = userInfo.Email,
                    Password = userInfo.Password.ComputeSha256Hash()
                };
                var users = await _dbContext.ReadJson<User>(_jsonfile);
                if (users == null)
                {
                    status = await _dbContext.WriteJson<User>(newUser, _jsonfile);
                    if (status)
                    {
                        return new Response
                        {
                            Message = "Successfully registerd",
                            Success = true
                        };
                    }
                    else
                    {
                        return new Response
                        {
                            Message = "Unable to save user info",
                            Success = false
                        };
                    }
                }
                //check if the user exist
                var exist = users!.SingleOrDefault(x => x.Email == userInfo.Email);
                if (exist != null)
                {
                    return new Response
                    {
                        Message = "User already exist",
                        Success = false
                    };
                }                    
                // Register user
                var isRegistered = await _dbContext.WriteJson<User>(newUser, _jsonfile);
                if (isRegistered)
                {
                    return new Response
                    {
                        Message = "Successfully registerd",
                        Success = true,
                    };
                }           
                return new Response
                {
                    Message = "An error occured",
                    Success = false
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> Login(LoginViewModel userInfo)
        {
            try
            {
              var users = await _dbContext.ReadJson<User>(_jsonfile);
              if(users != null)
                {
                  foreach(var user in users)
                    {
                        if(userInfo.Email == user.Email && userInfo.Password.ComputeSha256Hash() == user.Password)
                        {
                            return user;
                        }
                    }
                }
              return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
 
    }
}
