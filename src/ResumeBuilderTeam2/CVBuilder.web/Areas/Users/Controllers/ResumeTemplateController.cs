using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class ResumeTemplateController : Controller
    {
        public IActionResult Template1()
        {
            return View();
        }
    }
}
