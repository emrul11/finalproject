using CVBuilder.Domain.CVEntites.BaseEntites;
using CVBuilder.Domain.CVEntites.Sections;

namespace CVBuilder.Web.Areas.Users.Models
{
    public class References
    {
        public string Designation { get; set; }
        public string Name { get; set; }
    }
    public class CVBuilderDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public ProfessionalSummarySection ProfessionalSummary { get; set; }
        public List<string> Skills { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }

        public List<Project>? Projects { get; set; }
        public List<ProfessionalTrainingSection> Trainning { get; set; }

        public List<Education> Education { get; set; }

        public List<References> References { get; set; }

       



        //public int Id { get; set; }
        //public string FullName { get; set; }
        //public string Email { get; set; }
        //public string ContactNumber { get; set; }
        //public string Skype { get; set; }
        //public string LinkedIn { get; set; }
        //public string ResidentialAddress { get; set; }
        //public string ProfessionalSummary { get; set; }

        //// Navigation properties
        //public ICollection<LanguageSkill> LanguageSkills { get; set; }
        //public ICollection<WorkExperience> WorkExperiences { get; set; }
        //public ICollection<IndustrialProject> IndustrialProjects { get; set; }
        //public ICollection<Training> Trainings { get; set; }
        //public ICollection<Education> Educations { get; set; }
        //public ICollection<Reference> References { get; set; }

    }
}
