using Medik.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Medik.Infrastructure
{
    public class CommentRepository : ICommentRepository
    {
        public IConfiguration _configuration { get; }
        public string connectionString { get; set; }
        SqlConnection con;
        public CommentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public List<Comment> GetAllComment()
        {
            List<Comment> comments = new List<Comment>();
            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllComments", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Comment comment = new Comment()
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        FullName = rdr["FullName"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Age = Convert.ToInt32(rdr["Age"]),
                        PhoneNumber = rdr["PhoneNumber"].ToString(),
                        Profession = rdr["Profession"].ToString(),
                        Degree = rdr["Degree"].ToString(),
                        Content = rdr["Contents"].ToString(),
                        Image = rdr["Image"].ToString(),
                        CreatedAt = DateTime.Parse(rdr["CreatedAt"].ToString()),
                        LastUpdatedAt = DateTime.Parse(rdr["LastUpdatedAt"].ToString())
                    };
                    comments.Add(comment);
                }
                con.Close();
            }
            return comments;
        }
        public Comment GetComment(int id)
        {
            Comment comment = new Comment();
            using (con = new SqlConnection(connectionString))
            {                
                SqlCommand cmd = new SqlCommand("spGetCommentById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CommentId", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    comment.Id = Convert.ToInt32(rdr["Id"]);
                    comment.FullName = rdr["FullName"].ToString();
                    comment.Email = rdr["Email"].ToString();
                    comment.Age = Convert.ToInt32(rdr["Age"]);
                    comment.PhoneNumber = rdr["PhoneNumber"].ToString();
                    comment.Profession = rdr["Profession"].ToString();
                    comment.Degree = rdr["Degree"].ToString();
                    comment.Content = rdr["Contents"].ToString();
                    comment.Image = rdr["Image"].ToString();
                    comment.CreatedAt = DateTime.Parse(rdr["CreatedAt"].ToString());
                    comment.LastUpdatedAt = DateTime.Parse(rdr["LastUpdatedAt"].ToString());
                }
                con.Close();
            }
            return comment;
        }
        public User GetUserProfile(string id)
        {
            User user = new User();
            using (con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.Id = rdr["Id"].ToString();
                    user.FirstName = rdr["FirstName"].ToString();
                    user.LastName = rdr["LastName"].ToString();
                    user.Email = rdr["Email"].ToString();                      
                }
                con.Close();
            }
            return user;
        }
    }
}
