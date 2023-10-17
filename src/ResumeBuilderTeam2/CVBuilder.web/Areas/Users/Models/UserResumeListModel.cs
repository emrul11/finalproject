using CVBuilder.Application.features.Services;
using CVBuilder.Domain.CVEntites;
using CVBuilder.Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Web;

namespace CVBuilder.Web.Areas.Users.Models
{
    public class UserResumeListModel
    {
        private readonly IResumeService _resumeService;

        public UserResumeListModel()
        {

        }

        public UserResumeListModel(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        public IList<Resume> GetUserResumes(Guid userId, int templateId)
        {
            return _resumeService.GetResumeByUserAndTemplateId(userId,templateId);
        }

       

        internal void DeleteCourse(Guid id)
        {
            _resumeService.DeleteCourse(id);
        }
    }
}
