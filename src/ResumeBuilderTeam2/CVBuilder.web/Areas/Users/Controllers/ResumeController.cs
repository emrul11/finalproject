
using Autofac;
using Crud.Persistance.Features.Membership;
using CVBuilder.Domain.CVEntites;
using CVBuilder.Web.Areas.Users.Factory;
using CVBuilder.Web.Areas.Users.Models;
using CVBuilder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CVBuilder.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class ResumeController : Controller
    {
       //  [Authorize]
        private readonly IHostingEnvironment _environment;
        private readonly IResumeFactory _resumeFactory;
        private readonly ILifetimeScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        public ResumeController(IHostingEnvironment hostingEnvironment, ILifetimeScope scope,
           IResumeFactory resumeFactory, UserManager<ApplicationUser> userManager)
        {
            _environment = hostingEnvironment;
            _resumeFactory = resumeFactory;     
            _scope = scope;
            _userManager = userManager;
        }
        public IActionResult ResumeBuilder()
        {
            //var model = new ResumeBuilderModel();
            return View();
        }


        public IActionResult ShowTemplateTwo()
        {
            var model = GetResumeByUserAndTempleteId(0);

            if (model != null)
            {
                var cvData = model.Result;
                ViewBag.Project = cvData.Projects;
                ViewBag.Skills = cvData.Skills.SkillsList;
                ViewBag.References = cvData.References;
                ViewBag.ResumeName = cvData.ResumeName;
                ViewBag.Education = cvData.Education;
                ViewBag.Introduction = cvData.Introduction;
                ViewBag.ProfessionalSummary = cvData.ProfessionalSummary;
                ViewBag.ProfessionalTraining = cvData.ProfessionalTraining;
                ViewBag.WorkExperiences = cvData.WorkExperiences;


            }

            return View("templete-two");
        }


        public IActionResult ShowTemplateOne()
        {
          var model =    GetResumeByUserAndTempleteId(0);

            if(model != null) {
                var cvData = model.Result;  
                ViewBag.Project = cvData.Projects;
                ViewBag.Skills = cvData.Skills.SkillsList;
                ViewBag.References = cvData.References;
                ViewBag.ResumeName = cvData.ResumeName;
                ViewBag.Education = cvData.Education;
                ViewBag.Introduction = cvData.Introduction;
                ViewBag.ProfessionalSummary = cvData.ProfessionalSummary;
                ViewBag.ProfessionalTraining = cvData.ProfessionalTraining;
                ViewBag.WorkExperiences = cvData.WorkExperiences;

             
            }
 
            return View("template-one");
        }


        [Authorize]
        public IActionResult Index()
        {  
            return View();
        }

   //     [HttpGet("GetResumeByUserAndTempleteId")]
        public async Task<Resume> GetResumeByUserAndTempleteId( int tempId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var model = _scope.Resolve<ResumeCreateModel>();
            var result = await model.GetCVByByUserIdWithTempateId(Guid.Parse(userId));
            return result;
        }


    
        public async Task<JsonResult> GetCVByUserIdWithTemplateId( )
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var model = _scope.Resolve<ResumeCreateModel>();
            model.ResolveDependency(_scope);
            //using the model according to sirs model
         var result  = await   model.GetCVByByUserIdWithTempateId(Guid.Parse(userId)); //fully function insertion resume
            return  new JsonResult(result);
        }
        // update 
        public async Task<JsonResult> UpdateCVByUserIdAndTemplateId( Resume cVTemplate, ResumeDTO CVData)
        {
            var model = _scope.Resolve<ResumeUpdateModel>();
            model.ResolveDependency(_scope);
           var result = await    model.CVUpdateByUserIdAndTemplateId(cVTemplate , CVData);
            return  new JsonResult(result);
        }

        // create 
        public async Task<JsonResult> CVBuilderAdd(ResumeDTO CVData)
        {
            var model = _scope.Resolve<ResumeCreateModel>();   
            model.ResolveDependency(_scope);
            var userId = _userManager.GetUserId(HttpContext.User);

          var existingCVData =   await model.GetCVByByUserIdWithTempateId( Guid.Parse(userId));
            if(existingCVData != null)
            {
                // update CV 
                try
                {
                    var updateModel = _scope.Resolve<ResumeUpdateModel>();
                    updateModel.ResolveDependency(_scope);
                    // CVData is updated Data
                    var updateResult = await updateModel.CVUpdateByUserIdAndTemplateId(existingCVData, CVData);
                    return new JsonResult("data has been updated");
                }
                catch(Exception ex)
                {
                    return null;
                }
            }


            var cvtemplate = await _resumeFactory.PrepareResume(CVData);
            cvtemplate.UserId = new Guid(userId);//for testing purpose
            //var model = 
            model.CVTemplate = cvtemplate;//using the model according to sirs model
            model.CreateResume(); //fully function insertion resume
            return new JsonResult("Add Successful");
        }

        public async Task<JsonResult> ImageUpload()
        {
            string base64 = Request.Form["image"];
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            string filePath = Path.Combine(this._environment.WebRootPath, "images", $"{_userManager.GetUserId(HttpContext.User)}.png");
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            return new JsonResult($"{_userManager.GetUserId(HttpContext.User)}.png");
        }
    }
}
