using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites
{
    public class Reference : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public Resume CVTemplate { get; set; }
        public int CVTemplateId { get; set; }

    }
}
