using Autofac;
using Crud.Persistance.Features.Membership;
using CVBuilder.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Models
{
    public class RegisterModelVM
    {
        private  IEmailServiceTest _emailServiceTest;
        private  IConfiguration _configuration;
        public RegisterModelVM()
        {
            
        }
        public RegisterModelVM(IEmailServiceTest emailServiceTest, IConfiguration configuration)
        {
            _emailServiceTest = emailServiceTest;
            _configuration = configuration;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _emailServiceTest = scope.Resolve<IEmailServiceTest>();
            _configuration = scope.Resolve<IConfiguration>();

        }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Your Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string? ReturnUrl { get; set; }
        public bool IsRegisterd { get; set; }
        [Display(Name ="Chose a Profile Picture")]
        public IFormFile? ProfilePicture {  get; set; }
        public string? ProfilePictureUrl { get; set; }

        public async Task SendEmailConfitmationEmail(ApplicationUser user, string code)
        {
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            
            UserEmailOptions emailOptions = new UserEmailOptions()
            {
                ToEmail = new List<string> {user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink,user.Id, code))
                }
            };
            await _emailServiceTest.SendEmailConfirmation(emailOptions);
        }
    }
}
