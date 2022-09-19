using Medik.Core.Interfaces;
using Medik.Core.ViewModel;
using Medik.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Medik.Infrastructure.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly string PostFile = "Posts.json";
        private readonly IJsonOperations _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUploadFiles _uploadFiles;
        public List<Post> Posts { get; set; }
        public PostRepository(IJsonOperations dbContext,
            IHostingEnvironment hostingEnvironment,
            IUploadFiles uploadFiles)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
            _uploadFiles = uploadFiles;
        }
        public async Task<Post> GetPost(string Id)
        {
            try
            {
                var posts = await _dbContext.ReadJson<Post>(PostFile);
                return posts.FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Post>> GetAllPosts()
        {
            try
            {
              return await _dbContext.ReadJson<Post>(PostFile);
              
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AddPost(PostViewModel contents, string photo)
        {
            try
            {
                Post post = new Post()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = contents.Title,
                    Content = contents.Content,
                    Image = photo,
                    CreatedAt = DateTime.Now
                };
                return await _dbContext.WriteJson<Post>(post, PostFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdatePost(EditViewModel model)
        {
            try
            {
                List<Post> posts = await _dbContext.ReadJson<Post>(PostFile);
                Post post = posts.FirstOrDefault(x => x.Id == model.Id);
                post.Title = model.Title;
                post.Content = model.Content;
                post.LastUpdatedAt = DateTime.Now;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images",
                            model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    post.Image = _uploadFiles.Upload(model);
                }
                return await _dbContext.UpdateJson<Post>(posts, PostFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeletePost(string Id)
        {
            try
            {
                List<Post> posts = await _dbContext.ReadJson<Post>(PostFile);
                List<Post> newPosts = posts.Where(x => x.Id != Id).ToList();
                return await _dbContext.UpdateJson<Post>(newPosts, PostFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
