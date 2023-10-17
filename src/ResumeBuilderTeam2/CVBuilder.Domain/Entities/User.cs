using CVBuilder.Domain.CVEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.Entities
{
    public class User : BaseUser
    {
        public List<Resume> CVTemplates { get; set; }
    }
}
