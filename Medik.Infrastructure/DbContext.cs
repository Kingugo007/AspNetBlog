using Medik.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Medik.Infrastructure
{
    public class DbContext : IDbContext
    {
        public IConfiguration _configuration { get; }
        public string connectionString { get; set; }
        SqlConnection con;
        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public List<Post> GetAllPost()
        {
            List<Post> posts = new List<Post>();
            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllPosts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Post post = new Post()
                    {
                        Id = rdr["Id"].ToString(),
                        Title = rdr["Title"].ToString(),
                        Content = rdr["Contents"].ToString(),
                        Image = rdr["image"].ToString(),
                        CreatedAt = DateTime.Parse(rdr["CreatedAt"].ToString()),
                        LastUpdatedAt = DateTime.Parse(rdr["LastUpdatedAt"].ToString())
                    };
                    posts.Add(post);
                }
                con.Close();
            }
            return posts;
        }
        public Post GetPost(string id)
        {
            Post post = new Post();
            using (con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetPostById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PostId", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    post.Id = rdr["Id"].ToString();
                    post.Title = rdr["Title"].ToString();
                    post.Content = rdr["Contents"].ToString();
                    post.Image = rdr["image"].ToString();
                    post.CreatedAt = DateTime.Parse(rdr["CreatedAt"].ToString());
                    post.LastUpdatedAt = DateTime.Parse(rdr["LastUpdatedAt"].ToString());
                }
                con.Close();
            }
            return post;
        }



    }
}
