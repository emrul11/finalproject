using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Domain.CVEntites
{
    public class ResumeTemplate 
    {
        [Key]
        public int Id { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateThumbNailUrl { get; set; }
    }
}
