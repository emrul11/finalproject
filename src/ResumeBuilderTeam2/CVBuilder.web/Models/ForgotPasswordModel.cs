using Autofac;
using Crud.Persistance.Features.Membership;
using CVBuilder.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailSent { get; set; }

        private IEmailServiceTest _emailServiceTest;
        private IConfiguration _configuration;
        public ForgotPasswordModel()
        {

        }
        public ForgotPasswordModel(IEmailServiceTest emailServiceTest, IConfiguration configuration)
        {
            _emailServiceTest = emailServiceTest;
            _configuration = configuration;
        }
        internal void ResolveDependency(ILifetimeScope scope)
        {
            _emailServiceTest = scope.Resolve<IEmailServiceTest>();
            _configuration = scope.Resolve<IConfiguration>();

        }

        public async Task SendResetPasswordEmail(ApplicationUser user, string code)
        {
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;

            UserEmailOptions emailOptions = new UserEmailOptions()
            {
                ToEmail = new List<string> { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink,user.Id, code))
                }
            };
            await _emailServiceTest.SendEmailResetPassword(emailOptions);
        }
    }
}
