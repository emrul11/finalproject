using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.Sections
{
    public class ProfessionalSummarySection : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ProfessionalSummary { get; set; }

        public Resume CVTemplate { get; set; }

        public int CVTemplateId { get; set; }
    }
}
