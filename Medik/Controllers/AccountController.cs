using Medik.Core.Interfaces;
using Medik.Core.ViewModel;
using Medik.Domain.Model;
using Medik.Infrastructure;
using Medik.Infrastructure.AccountRepository;
using Medik.Infrastructure.ProfileRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Medik.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRespository;
        private readonly IProfileRepository _profileRespository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAuthServices _authServices;
        private readonly ICommentRepository _commentRepository;

        public AccountController(IAccountRepository accountRespository,
            IProfileRepository profileRepository,
            IHttpContextAccessor accessor,
            IAuthServices authServices,
            ICommentRepository commentRepository

            )
        {
            _accountRespository = accountRespository;
            _profileRespository = profileRepository;
            _accessor = accessor;
            _authServices = authServices;
            _commentRepository = commentRepository;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var added = await _authServices.Register(userInfo);
                if (added != null)
                {
                   return LocalRedirect("/Account/Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Email Already Exist");
                    return View("Register", userInfo);
                }
            }
            ModelState.AddModelError(string.Empty,"An Error occured");
            return View("Register", userInfo);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userInfo)
        {
            if (ModelState.IsValid)
                {
                //Set the Expiry date of the Cookie.
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                //Create a Cookie with a suitable Key and add the Cookie to Browser.
                Response.Cookies.Append("Key", userInfo.Email, option);
                var user = await _authServices.Login(userInfo);
                    if (user != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return View("Login", userInfo);
     
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            string identity = _accessor.HttpContext.Request.Cookies["Key"];
            if(identity != null)
            {
                DashboardViewModel info = new DashboardViewModel()
                {
                    Identity = identity,
                    user = _commentRepository.GetUserProfile(identity),
                };
                return View(info);
            }
            return LocalRedirect("/Account/Login");
        }
        [HttpPost] 
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Key");           
            return LocalRedirect("/Account/Login");
        }

    }
}
