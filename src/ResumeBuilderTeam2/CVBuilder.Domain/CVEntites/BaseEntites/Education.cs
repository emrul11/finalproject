using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites
{
    public class Education: IEntity<int>
    {
        public int Id { get; set; }
        public string? Degree { get; set; }
        public string? Institution { get; set; }
        public int? GraduationYear { get; set; }
        public double? CGPA { get; set; }
        public string? MajorField { get; set; }

        public Resume CVTemplate { get; set; }
        public int CVTemplateId { get; set; }

    }
}
