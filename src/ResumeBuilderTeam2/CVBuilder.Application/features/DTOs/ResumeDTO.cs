namespace CVBuilder.Application.features.DTOs
{
    public class SocialMediaDTO
    {
        public string SocialMediaName { get;  set; }
        public string LinkOrText { get; set; }
    }

    public class WorkExperienceDescription
    {
        public string Item { get; set; }
    }

    public class WorkExperienceDTO
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string StartDate { get; set; }
        public List<WorkExperienceDescription> Description { get; set; }
    }

    public class ProjectDTO
    {
        public string ProjectType { get; set; }
        public string ProjectPersonal { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string ProjectCompanyDescription { get; set; }
    }

    public class EducationDTO
    {
        public string Institution { get; set; }
        public string GraduationYear { get; set; }
    }

    public class ReferenceDTO
    {
        public string Designation { get; set; }
        public string Name { get; set; }
    }

    public class ProfessionalSummaryDTO
    {
        public string Title { get; set; }
        public string ProfessionalSummaryText { get; set; }
    }

    public class TrainingDTO
    {
        public string TrainingCourses { get; set; }
    }
    public class ResumeDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<SocialMediaDTO> SocialMediaList { get; set; }
        public List<string> Skills { get; set; }
        public List<WorkExperienceDTO> WorkExperiences { get; set; }
        public List<ProjectDTO> Projects { get; set; }
        public List<EducationDTO> Education { get; set; }
        public List<ReferenceDTO> References { get; set; }
        public ProfessionalSummaryDTO ProfessionalSummary { get; set; }
        public List<TrainingDTO> Trainning { get; set; }
    }
}
