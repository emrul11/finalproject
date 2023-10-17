using CVBuilder.Domain.CVEntites.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites.ItemLists
{
    public class SkillListItems
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public LanguageFrameworkSkillsSection SkillsSection { get; set; }
        public int SkillsSectionId { get; set; }
    }
}
