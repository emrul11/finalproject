using CVBuilder.Domain.CVEntites;
using CVBuilder.Domain.CVEntites.BaseEntites;
using CVBuilder.Domain.CVEntites.BaseEntites.ItemLists;
using CVBuilder.Domain.CVEntites.Sections;
using CVBuilder.Web.Areas.Users.Models;
using NuGet.ProjectModel;

namespace CVBuilder.Web.Areas.Users.Factory
{
    public class ResumeFactory : IResumeFactory
    {
        public async Task<Resume> PrepareResume(ResumeDTO resumeDTO)
        {

            var model = new Resume();

            await PrepareIntroductionSection(resumeDTO, model);

            await PrepareProfessionalSummarySection(resumeDTO, model);

            await PrepareLanguageFrameworkSkillsSection(resumeDTO, model);

            await PrepareWorkExperiences(resumeDTO, model);

            await PrepareProjects(resumeDTO, model);

            await PrepareProfessionalTraining(resumeDTO, model);

            await PrepareEducation(resumeDTO, model);

            await PrepareReferences(resumeDTO, model);

            return model;
        }

        public async Task<Resume> PrepareIntroductionSection(ResumeDTO resumeDTO, Resume model)
        {

            model.Introduction.IntroName = resumeDTO.Name;
            model.Introduction.IntroEmail = resumeDTO.Email;
            model.Introduction.IntroContact = resumeDTO.Mobile;
            model.Introduction.ImageURL = resumeDTO.ImageURL;
            foreach(var social in resumeDTO.SocialMediaList)
            {
                model.Introduction.SocialMediaList.Add(
                    new SocialMedia { LinkOrText = social.LinkOrText,SocialMediaName = social.SocialMediaName}
                    );

            }


            return model;
        }
        public async Task<Resume> PrepareProfessionalSummarySection(ResumeDTO resumeDTO, Resume model)
        {

            //model.ProfessionalSummary.ProfessionalSummary = resumeDTO.ProfessionalSummary.ProfessionalSummaryText;
            model.ProfessionalSummary.ProfessionalSummary = "afasfasdfa";

            model.ProfessionalSummary.Title = resumeDTO.ProfessionalSummary.Title;

            return model;
        }
        public async Task<Resume> PrepareLanguageFrameworkSkillsSection(ResumeDTO resumeDTO, Resume model)
        {
            model.Skills.Title = "Skills";
            foreach (var skill in resumeDTO.Skills)
            {
                model.Skills.SkillsList.Add(new SkillListItems { Description = skill });
            }
            return model;
        }
        public async Task<Resume> PrepareWorkExperiences(ResumeDTO resumeDTO, Resume model)
        {
            foreach(var wo in resumeDTO.WorkExperiences)
            {
                var temp = new WorkExperience
                {
                    JobTitle = wo.JobTitle,
                    Company = wo.Company,
                    StartDate = wo.StartDate
                };
                foreach (var item in wo.Description)
                {
                    temp.DescriptionItems.Add(new WorkExperienceDescriptionItems { Description = item.Item });
                }
                model.WorkExperiences.Add(temp); 

            }

            return model;
        }
        public async Task<Resume> PrepareProjects(ResumeDTO resumeDTO, Resume model)
        {
            model.Projects.Title = "Projects";
            foreach (var project in resumeDTO.Projects)
            {
                var temp = new Project { Description = project.Description ,
                    Title = project.Title,
                ProjectType = project.ProjectType,
                TechStack = "C#"
                
                };
                model.Projects.Projects.Add(temp);
            }
            return model;
        }
        public async Task<Resume> PrepareProfessionalTraining(ResumeDTO resumeDTO, Resume model)
        {
            model.ProfessionalTraining.Title = "Tranning";
            foreach (var training in resumeDTO.Trainning)
            {
                var temp = new TrainingListItems
                {
                    Description = training.TrainingCourses
                };
                model.ProfessionalTraining.TrainingItemList.Add(temp);
            }
            return model;
        }

        public async Task<Resume> PrepareEducation(ResumeDTO resumeDTO, Resume model)
        {
            foreach (var ed in resumeDTO.Education)
            {
                model.Education.Add(new Education { Institution =ed.Institution, 
                    GraduationYear = int.Parse(ed.GraduationYear)});
            }
            return model;
        }


        public async Task<Resume> PrepareReferences(ResumeDTO resumeDTO, Resume model)
        {

            foreach (var rf in resumeDTO.References)
            {
                model.References.Add(new Reference
                {
                    Designation = rf.Designation,
                    Name = rf.Name
                });
            }
            return model;
        }

    }
}