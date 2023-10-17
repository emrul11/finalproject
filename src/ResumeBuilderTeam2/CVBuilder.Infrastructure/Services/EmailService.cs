using CVBuilder.Domain.Utilities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CVBuilder.Infrastructure
{
    public class EmailService : IEmailServiceTest
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigure _smtpConfig;
        public EmailService(IOptions<SMTPConfigure> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        public async Task SendBulkEmail(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = UpdatePlaceHolder("Hello {{UserName}}, Test mail", emailOptions.PlaceHolders);
            emailOptions.Body = UpdatePlaceHolder(GetEmailBody("EmailConfirmation"), emailOptions.PlaceHolders);
            await SendEmail(emailOptions);
        }
        //Email Confirmation
        public async Task SendEmailConfirmation(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = UpdatePlaceHolder("Hello {{UserName}}, Confirm Tour Email Id", emailOptions.PlaceHolders);
            emailOptions.Body = UpdatePlaceHolder(GetEmailBody("EmailConfirmation"), emailOptions.PlaceHolders);
            await SendEmail(emailOptions);
        }
        //Reset Password
        public async Task SendEmailResetPassword(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = UpdatePlaceHolder("Hello {{UserName}}, Reset Your Password", emailOptions.PlaceHolders);
            emailOptions.Body = UpdatePlaceHolder(GetEmailBody("ForgotEmail"), emailOptions.PlaceHolders);
            await SendEmail(emailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            var mailMessage = new MailMessage()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };
            foreach (var toEmail in userEmailOptions.ToEmail)
            {
                mailMessage.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mailMessage.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mailMessage);
        }
        //Template Selection
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
        private string UpdatePlaceHolder(string text, List<KeyValuePair<string, string>> keyValuepairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuepairs != null) 
            { 
                foreach (var placeholder in keyValuepairs) { 
                    if(text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }
    }
}
