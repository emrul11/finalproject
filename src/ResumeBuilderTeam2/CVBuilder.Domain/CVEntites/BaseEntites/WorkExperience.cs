using CVBuilder.Domain.CVEntites.BaseEntites.ItemLists;
using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites
{

    public class WorkExperience : IEntity<int>
    {
        public WorkExperience()
        {
            DescriptionItems = new List<WorkExperienceDescriptionItems>();
        }

        [Key]
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string StartDate { get; set; }
        public List<WorkExperienceDescriptionItems> DescriptionItems { get; set; }

        public Resume CVTemplate { get; set; }
        public int CVTemplateId { get; set; }

    }
}
