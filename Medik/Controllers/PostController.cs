using Medik.Core.Interfaces;
using Medik.Core.ViewModel;
using Medik.Domain.Model;
using Medik.Infrastructure.PostRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Medik.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepsitory;
        private readonly IUploadFiles _uploadFiles;
        private readonly IHttpContextAccessor _accessor;
        public PostController(IPostRepository postRepsitory,
             IUploadFiles uploadFiles,
             IHttpContextAccessor accessor         
            )
        {
            _postRepsitory = postRepsitory;
            _uploadFiles = uploadFiles;
            _accessor = accessor;        
        }
        [HttpGet]
        public IActionResult Post()
        {
            string identity = _accessor.HttpContext.Request.Cookies["Key"];
            if (identity != null)
            {
                PostViewModel info = new PostViewModel()
                {
                    Identity = identity,                    
                };
                return View(info);
            }
            return LocalRedirect("/Account/Login");
          
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostViewModel contents)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if(contents.Photo != null)
                {
                    fileName = _uploadFiles.Upload(contents);                              
                }
                var res = await _postRepsitory.AddPost(contents, fileName);
                if (!res)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                }
                return LocalRedirect("/Home/Index");
            }
            return View(contents);
        }
        [HttpGet]    
        public async Task<IActionResult> Edit(string id)
        {
            string identity = _accessor.HttpContext.Request.Cookies["Key"];
            Post post = await _postRepsitory.GetPost(id);
            EditViewModel edit = new EditViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ExistingPhotoPath = post.Image,
                Identity = identity,
                
            };
            return View(edit);  
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel contents)
        {
            if (ModelState.IsValid)
            {
               var res  = await _postRepsitory.UpdatePost(contents);
                if (!res)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                }
                return LocalRedirect("/Home/Index");
            }
            return View(contents);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var res = await _postRepsitory.DeletePost(id);
                if (!res)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                }
                return LocalRedirect("/Home/Index");
            }
            return View();
        }


    }
}
