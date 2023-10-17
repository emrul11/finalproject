namespace CVBuilder.Infrastructure
{
    public interface IEmailServiceTest
    {
        Task SendBulkEmail(UserEmailOptions emailOptions);
        Task SendEmailConfirmation(UserEmailOptions emailOptions);
        Task SendEmailResetPassword(UserEmailOptions emailOptions);
    }
}