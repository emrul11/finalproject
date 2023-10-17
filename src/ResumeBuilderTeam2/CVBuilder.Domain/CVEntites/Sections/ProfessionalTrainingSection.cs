using CVBuilder.Domain.CVEntites.BaseEntites.ItemLists;
using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.Sections
{
    public class ProfessionalTrainingSection : IEntity<int>
    {
        public ProfessionalTrainingSection()
        {
            TrainingItemList = new List<TrainingListItems>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TrainingListItems>? TrainingItemList { get; set; }

        public Resume CVTemplate { get; set; }
        public int CVTemplateId { get; set; }



    }
}
