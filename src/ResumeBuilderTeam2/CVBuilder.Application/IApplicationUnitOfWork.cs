using CVBuilder.Application.features.ResumeInterfaces;
using CVBuilder.Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application
{
    public interface IApplicationUnitOfWork:IUnitOfWork
    {
        IResumeRepository Resumes { get; }
    }
}
