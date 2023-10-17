using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Infrastructure.Securities
{
    public class ManageUserViewRequirementHandler : AuthorizationHandler<ManageUserViewRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageUserViewRequirement requirement)
        {
            if(context.User.IsInRole("Super Admin") || 
                context.User.HasClaim(claim => claim.Type == "Admin" && claim.Value == "Edit User"))
            {
                context.Succeed(requirement); 
            }
            return Task.CompletedTask;
        }
    }
}
