using CVBuilder.Domain.CVEntites.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites.BaseEntites.ItemLists
{
    public class TrainingListItems
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ProfessionalTrainingSection TrainingSection { get; set; }
        public int TrainingSectionId { get; set; }
    }
}
