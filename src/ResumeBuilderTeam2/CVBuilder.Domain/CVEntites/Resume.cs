using CVBuilder.Domain.CVEntites.BaseEntites;
using CVBuilder.Domain.CVEntites.Sections;
using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites
{
    public class Resume: IEntity<int>
    {
        public Resume() {
            Introduction = new IntroductionSection();
            ProfessionalSummary = new ProfessionalSummarySection();
            Skills =  new LanguageFrameworkSkillsSection();
            WorkExperiences = new List<WorkExperience>();
            Projects = new ProjectsSection();
            ProfessionalTraining = new ProfessionalTrainingSection();
            Education = new List<Education> ();
            References = new List<Reference> ();
        }
        public int Id { get; set; }
        public IntroductionSection Introduction { get; set; }
        public ProfessionalSummarySection ProfessionalSummary { get; set; }
        public LanguageFrameworkSkillsSection Skills { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
        public ProjectsSection Projects { get; set; }
        public ProfessionalTrainingSection ProfessionalTraining { get; set; }
        public List<Education> Education { get; set; }
        public List<Reference> References { get; set; }
        public Guid UserId { get; set; }
        public int ResumeTemplteId { get; set; }
        public string? ResumeName { get; set; }
    }
}
