using CVBuilder.Domain.CVEntites.Sections;
using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites
{
    public class SocialMedia : IEntity<int>
    {
        public int Id { get; set; }
        public string SocialMediaName { get; set; }
        public string LinkOrText { get; set; }

        public IntroductionSection introductionSection { get; set; }
        public int introductionSectionId { get; set; }

        //icon

    }
}
