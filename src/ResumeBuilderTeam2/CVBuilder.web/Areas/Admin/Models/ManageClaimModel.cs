using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Areas.Admin.Models
{
    public class ManageClaimModel
    {
        public string Id { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IList<string> Claims { get; set; }

        public ManageClaimModel()
        {
            Claims = new List<string>();
        }
        

    }
}
