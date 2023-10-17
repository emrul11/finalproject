using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Web.Areas.Admin.Models
{
    public class IdentityUserListModel
    {
        private  UserManager<ApplicationUser> _userManager;

        //public IQueryable<ApplicationUser> Users { get; set; }

        public IdentityUserListModel()
        {
            
        }
        public IdentityUserListModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            //Users = new IQueryable<ApplicationUser>();
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }

        internal void GetAllIdentityUsers()
        {
            var Users = _userManager.Users;
           
            
        }
    }
}
