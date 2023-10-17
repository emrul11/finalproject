using CVBuilder.Domain.CVEntites.BaseEntites.ItemLists;
using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.Sections
{
    public class LanguageFrameworkSkillsSection : IEntity<int>
    {
        public LanguageFrameworkSkillsSection()
        {
            SkillsList = new List<SkillListItems>();
        }
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<SkillListItems> SkillsList { get; set; }
        public int CVTemplateId { get; set; }
        public Resume CVTemplate { get; set; }


    }
}
