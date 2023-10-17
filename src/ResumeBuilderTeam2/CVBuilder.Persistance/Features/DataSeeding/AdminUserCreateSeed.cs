using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Persistence.Features.DataSeeding
{
    internal static class AdminUserCreateSeed
    {
        public static ApplicationUser Admins
        {
            get
            {
                var adminUser = new ApplicationUser()
                {
                    Id = new Guid("A41D000C-5FB1-47B7-ADDF-F0C73FAEBEEA"),
                    FirstName = "Jhon",
                    LastName = "Deo",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com".ToUpper(),
                    NormalizedUserName = "admin@gmail.com".ToUpper(),
                    SecurityStamp = new Guid().ToString(),
                };
                adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "Abc123");
                return adminUser;
            }
        }
    }
}
