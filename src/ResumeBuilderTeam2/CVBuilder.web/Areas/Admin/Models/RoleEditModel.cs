using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Areas.Admin.Models
{
    public class RoleEditModel
    {
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }

        public IList<string> Users { get; set; }

        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        public RoleEditModel()
        {
            
        }
        public RoleEditModel(RoleManager<ApplicationRole> roleManager,  UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            Users = new List<string>();
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
                Id = Convert.ToString(role.Id);
                RoleName = role.Name;
            }
            foreach(var user in _userManager.Users)
            {
              if( await _userManager.IsInRoleAsync(user, role.Name))
                {
                    Users.Add(user.UserName);
                }
            }
        }

        internal async Task EditRoleAsync()
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                role.Name = RoleName;
                await _roleManager.UpdateAsync(role);
            }
        }
    }
}
