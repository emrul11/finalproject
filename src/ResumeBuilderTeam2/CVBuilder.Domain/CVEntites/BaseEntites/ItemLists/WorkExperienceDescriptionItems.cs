using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites.ItemLists
{
    public class WorkExperienceDescriptionItems
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public WorkExperience WorkExperience { get; set; }
        public int WorkExperiencId { get; set; }
    }
}
