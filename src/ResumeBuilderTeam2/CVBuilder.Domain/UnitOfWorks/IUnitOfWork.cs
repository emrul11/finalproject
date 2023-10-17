using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.UnitOfWorks
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
    }
}
