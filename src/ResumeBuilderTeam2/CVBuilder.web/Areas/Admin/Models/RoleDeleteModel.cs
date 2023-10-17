using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Areas.Admin.Models
{
    public class RoleDeleteModel
    {
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }

        private RoleManager<ApplicationRole> _roleManager;
        public RoleDeleteModel()
        {
            
        }
        public RoleDeleteModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
        }

        internal async Task LoadRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                RoleName = role.Name;
            }
            
        }

        internal async Task DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {             
                await _roleManager.DeleteAsync(role);
            }
        }

        
    }
}
