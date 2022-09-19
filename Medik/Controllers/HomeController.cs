using Medik.Core.ViewModel;
using Medik.Infrastructure;
using Medik.Infrastructure.PostRepository;
using Medik.Infrastructure.ProfileRepository;
using Medik.Infrastructure.RecordRepository;
using Medik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Medik.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRespository;
        private readonly IProfileRepository _profileRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IRecordRepository _record;
        private readonly IDbContext _dbContext;
        private readonly ICommentRepository _commentRepository;

        public HomeController(ILogger<HomeController> logger,
            IPostRepository postRespository,
            IProfileRepository profileRepository,
            IHttpContextAccessor accessor,
            IRecordRepository record,
            IDbContext dbContext,
            ICommentRepository commentRepository
            )
        {
            _logger = logger;
            _postRespository = postRespository;
            _profileRepository = profileRepository;
            _accessor = accessor;
            _record = record;
            _dbContext = dbContext;
            _commentRepository = commentRepository;
        }
        [HttpGet]   
        public IActionResult Index()
        {
            string identity = _accessor.HttpContext.Request.Cookies["Key"];
            DetailsViewModel detailsViewModel = new DetailsViewModel()
            {
                Posts = _dbContext.GetAllPost(),
                Comments = _commentRepository.GetAllComment(),
                Identity = identity                
            };
            return View(detailsViewModel);
        }
        public IActionResult Details(string id)
        {
            string identity = _accessor.HttpContext.Request.Cookies["Key"];
            DetailsViewModel post = new DetailsViewModel()
            {
             Post = _dbContext.GetPost(id), 
             Identity = identity,
            };
            return View(post);
        }
        public IActionResult Profile(int id)
        {
            DetailsViewModel comment = new DetailsViewModel()
            {
                Comment = _commentRepository.GetComment(id),
            };
            return View(comment);
        }      
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
