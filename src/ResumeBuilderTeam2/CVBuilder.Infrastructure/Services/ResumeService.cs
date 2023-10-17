using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVBuilder.Application;
using CVBuilder.Application.features.Services;
using CVBuilder.Domain.CVEntites;

namespace CVBuilder.Infrastructure.Service
{
    public class ResumeService : IResumeService
    {

        private readonly IApplicationUnitOfWork _unitOfWork;
        public ResumeService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Resume> GetResumeByUserAndTemplateId(Guid userId)
        {
            var result= await _unitOfWork.Resumes.GetCVByUserId(userId);
           return result;
        }

      //  public void InsertResume(CVTemplate Resume)

        //used for specific CV
		public Resume GetCvTemplate(int id)
		{
           return _unitOfWork.Resumes.GetById(id);
		}

		public void InsertResume(Resume Resume)

        {
            
            _unitOfWork.Resumes.Add(Resume);
            _unitOfWork.Save();
        }

        public async Task<bool> UpdateResume(Resume Resume)
        {
            //var result =  _unitOfWork.Resumes.UpdateCVByUserIdAndTempId(Resume);
            try
            {
                var result = _unitOfWork.Resumes.EditAsync(Resume);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
          
        }
    }
}
