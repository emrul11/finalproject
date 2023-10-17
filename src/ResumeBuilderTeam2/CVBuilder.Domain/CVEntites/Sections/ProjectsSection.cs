using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVBuilder.Domain.CVEntites.BaseEntites;
using CVBuilder.Domain.Entities;

namespace CVBuilder.Domain.CVEntites.Sections
{
    public class ProjectsSection : IEntity<int>
    {
        public ProjectsSection() { 
            Projects = new List<Project>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Project> Projects { get; set; }

        public Resume CVTemplate { get; set; }
        public int CVTemplateId { get; set; }

    }
}
