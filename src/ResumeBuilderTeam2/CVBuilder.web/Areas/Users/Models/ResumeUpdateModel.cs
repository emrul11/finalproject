using Autofac;
using CVBuilder.Application.features.Services;
using CVBuilder.Domain.CVEntites;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Areas.Users.Models
{
    public class ResumeUpdateModel
    {
        public Resume CVTemplate { get; set; }
        private IResumeService _resumeService;
        public ResumeUpdateModel()
        {

        }
        public ResumeUpdateModel(IResumeService resumeService)
        {
            _resumeService = resumeService;          
        }
   
        public void ResolveDependency(ILifetimeScope scope)
        {
            _resumeService = scope.Resolve<IResumeService>();
        }
        public  async Task<JsonResult> CVUpdateByUserIdAndTemplateId(Resume exstingCvData, ResumeDTO updatedData)
        {
            var result = await _resumeService.UpdateResume(exstingCvData);
            if (result) return  new JsonResult(exstingCvData);
            return new JsonResult("data con not update somthig problem");
        }

    }
}
