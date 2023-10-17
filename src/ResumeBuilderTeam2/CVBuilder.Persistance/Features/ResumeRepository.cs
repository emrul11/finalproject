using CVBuilder.Application.features.ResumeInterfaces;
using CVBuilder.Domain.CVEntites;
using CVBuilder.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Persistence.Features
{
    public class ResumeRepository : Repository<Resume, int>, IResumeRepository
    {
        protected IApplicationDbContext _dbContext;
        protected DbSet<Resume> _dbSet;
        protected int CommandTimeout { get; set; }
        public ResumeRepository(IApplicationDbContext context) : base((DbContext)context)
        {
            _dbContext = context;
            _dbSet = _dbContext.CVTemplates;
        }

        public async Task<Resume> GetCVByUserId(Guid userId)
        {
            var result   = await  _dbSet.Where(x => x.UserId == userId)
                .Include(x => x.Education)
                .Include(x => x.Projects)
                .Include(x => x.WorkExperiences)
                .Include(x => x.References) 
                .Include(x => x.Skills)
                .Include(x => x.Skills.SkillsList)
                .Include(x => x.Introduction)
                .Include(x => x.ProfessionalSummary)
                .Include(x => x.ProfessionalTraining) 
                .Include(x => x.Introduction.SocialMediaList)
               
                .FirstOrDefaultAsync();

            if (result == null) return null;
 
            return result;
        }

        public async Task<bool> UpdateCVByUserIdAndTempId( Resume cVTemplate)
        {
           var result =  _dbSet.Update(cVTemplate);
            if (result == null) return false;
            return true;
           
        }
    }
}
