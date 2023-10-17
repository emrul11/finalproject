using CVBuilder.Domain.CVEntites.BaseEntites;
using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.Sections
{   public class IntroductionSection : IEntity<int>
    {
        public IntroductionSection()
        {
            SocialMediaList = new List<SocialMedia>();
        }
        public int Id { get; set; }
        public string? ImageURL { get; set; }
        public string IntroName { get; set; }
        public string IntroEmail { get; set; }
        public string? IntroContact { get; set; }       
        public List <SocialMedia> SocialMediaList { get; set; }
        public string? IntroAddress { get; set; }
        public Resume CVTemplate { get; set; }
        public int CVTemplateId { get; set; }
    }
}
