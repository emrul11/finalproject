using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Areas.Admin.Models
{
    public class RoleCreateModel
    {
        [Required]
        [Display(Name="Role Name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string RoleName { get; set; }

        private RoleManager<ApplicationRole> _roleManager;
        //empty constractor for resolve dependancy error
        public RoleCreateModel() { }


        //if we want we can write role create code here without action 
        public RoleCreateModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
        }
        public async Task<IdentityResult> CreateRole()
        {
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(RoleName))
            {
              result =  await _roleManager.CreateAsync(new ApplicationRole(RoleName));
                
            }
            return result;
        }
    }
}
