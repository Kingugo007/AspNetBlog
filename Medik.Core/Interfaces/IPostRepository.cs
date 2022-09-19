using Medik.Core.ViewModel;
using Medik.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medik.Infrastructure.PostRepository
{
    public interface IPostRepository
    {
        List<Post> Posts { get; set; }
        Task<Post> GetPost(string Id);
        Task<List<Post>> GetAllPosts();
        Task<bool> AddPost(PostViewModel contents, string fileName);
        Task<bool> UpdatePost(EditViewModel model);
        Task<bool> DeletePost(string id);
    }
}