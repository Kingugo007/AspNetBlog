using Medik.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Medik.Infrastructure
{
    public class AuthRepository : IAuthRepository
    {
        public IConfiguration _configuration { get; }
        public string connectionString { get; set; }
        SqlConnection con;
        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<User>> GeAllUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand("spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        User user = new User()
                        {
                            Id = rdr["Id"].ToString(),
                            FirstName = rdr["FirstName"].ToString(),
                            LastName = rdr["LastName"].ToString(),
                            Email = rdr["Email"].ToString(),
                            Password = rdr["Pass"].ToString(),
                            CreatedDate = DateTime.Parse(rdr["CreatedDate"].ToString())
                        };
                        users.Add(user);
                    }
                }
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
