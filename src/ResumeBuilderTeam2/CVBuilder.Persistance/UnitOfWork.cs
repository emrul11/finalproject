using CVBuilder.Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        protected readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => _dbContext = dbContext;
        public virtual void Save() => _dbContext?.SaveChanges();
        public virtual void Dispose() => _dbContext?.Dispose();

    }
}
