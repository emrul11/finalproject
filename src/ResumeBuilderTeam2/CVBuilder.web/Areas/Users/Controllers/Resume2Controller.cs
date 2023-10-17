using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class Resume2Controller : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Users/Views/Resume2/Index.cshtml");
        }
    }
}
