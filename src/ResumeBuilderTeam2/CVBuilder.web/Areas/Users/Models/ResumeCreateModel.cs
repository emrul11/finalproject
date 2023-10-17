using Autofac;
using CVBuilder.Application.features.Services;
using CVBuilder.Domain.CVEntites;

namespace CVBuilder.Web.Areas.Users.Models
{
    public class ResumeCreateModel
    {
        public Resume CVTemplate { get; set; }
        private IResumeService _resumeService;
        public ResumeCreateModel()
        {
        }
        public ResumeCreateModel(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _resumeService = scope.Resolve<IResumeService>();
        }
        internal void CreateResume()
        {
            _resumeService.InsertResume(CVTemplate);
        }

        public async Task<Resume> GetCVByByUserIdWithTempateId(Guid userId)
        {
          var result =  await  _resumeService.GetResumeByUserAndTemplateId(userId);
            return result;
        }
        internal Resume GetCV(int id)
        {
            Resume cv = _resumeService.GetCvTemplate( id);
            return cv;
		}
	}
}
