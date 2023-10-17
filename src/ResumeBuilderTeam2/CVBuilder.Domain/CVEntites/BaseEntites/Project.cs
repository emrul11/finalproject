using CVBuilder.Domain.CVEntites.Sections;
using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites
{
    public class Project : IEntity<int>
    {
        public int Id { get; set; }
        public string ProjectType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TechStack { get; set; }

        public ProjectsSection projectsSection { get; set; }
        public int projectsSectionId { get; set; }

    }
}
