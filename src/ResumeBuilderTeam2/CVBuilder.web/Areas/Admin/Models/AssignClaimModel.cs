using System.Security.Claims;

namespace CVBuilder.Web.Areas.Admin.Models
{
    public class AssignClaimModel
    {
        public string UserId { get; set; }
        public IList<UserClaim> Claims { get; set; }

        public AssignClaimModel()
        {
            Claims = new List<UserClaim>();
        }
    }



    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
    {
        new Claim("Create Claim", "Create Claim"),
        new Claim("Edit Claim","Edit Claim"),
        new Claim("Delete Claim","Delete Claim"),
        new Claim("View Claim","View Claim")
    };
    }
    public class UserClaim
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
