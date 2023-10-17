using CVBuilder.Domain.CVEntites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Crud.Persistance.Features.Membership
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public string? UserName { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
        //public List<CVTemplate> Templates { get; set; }



    }
}
