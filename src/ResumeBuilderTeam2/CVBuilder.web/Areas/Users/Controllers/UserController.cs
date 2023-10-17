using Autofac;
using Crud.Persistance.Features.Membership;
using CVBuilder.Web.Areas.Users.Models;
using CVBuilder.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CVBuilder.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class UserController : Controller
	{
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly ResumeCreateModel _resume;

        public UserController(ILifetimeScope scope, ILogger<UserController> logger,
        UserManager<ApplicationUser> userManager, ResumeCreateModel resume)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
           _resume = resume;
		}
        public IActionResult Index()
		{
			return View();
		}
        public IActionResult Profile()
        {
            var CV = _resume.GetCV(1);
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userId);
            ViewBag.user = user.Result;
            ViewBag.Photo = user.Result.ProfilePictureUrl;
            if(ViewBag.Photo == null)
            {
                ViewBag.Photo = "/images/ProfilePhoto/avatar.png";
			}
                return View();
        }
        public IActionResult ChoseTemplate()
        {
            return View();
        }
    }
}
