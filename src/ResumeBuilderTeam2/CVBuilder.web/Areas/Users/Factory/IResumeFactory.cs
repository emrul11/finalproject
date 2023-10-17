using CVBuilder.Domain.CVEntites;
using CVBuilder.Web.Areas.Users.Models;

namespace CVBuilder.Web.Areas.Users.Factory
{
    public interface IResumeFactory
    {
        public Task<Resume> PrepareResume(ResumeDTO resumeDTO);
    }
}
