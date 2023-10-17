using CVBuilder.Domain.Utilities;
using Microsoft.Extensions.Options;
using Moq;

namespace CVBuilder.Infrastructure.Tests
{
        [TestFixture]
        public class EmailServiceTests
        {
            private EmailService emailService;
            private Mock<IOptions<SMTPConfigure>> smtpConfigOptionsMock;

            [SetUp]
            public void Setup()
            {
                // Mock SMTPConfigure options
                var smtpConfig = new SMTPConfigure
                {
                    // Initialize with relevant configuration
                };

                smtpConfigOptionsMock = new Mock<IOptions<SMTPConfigure>>();
                smtpConfigOptionsMock.Setup(x => x.Value).Returns(smtpConfig);

                emailService = new EmailService(smtpConfigOptionsMock.Object);
            }

            [Test]
            public async Task SendBulkEmail_ValidInput_SendsEmail()
            {
                // Arrange
                var emailOptions = new UserEmailOptions
                {
                    // Initialize with relevant email options and placeholders
                };

                // Act
                await emailService.SendBulkEmail(emailOptions);

                // Assert
                // You can assert that the email sending logic is correct,
                // for example, by mocking SmtpClient and verifying its methods.
            }

            [Test]
            public async Task SendEmailConfirmation_ValidInput_SendsEmail()
            {
                // Arrange
                var emailOptions = new UserEmailOptions
                {
                    // Initialize with relevant email options and placeholders
                };

                // Act
                await emailService.SendEmailConfirmation(emailOptions);

                // Assert
                // You can assert that the email sending logic is correct,
                // for example, by mocking SmtpClient and verifying its methods.
            }

            [Test]
            public async Task SendEmailResetPassword_ValidInput_SendsEmail()
            {
                // Arrange
                var emailOptions = new UserEmailOptions
                {
                    // Initialize with relevant email options and placeholders
                };

                // Act
                await emailService.SendEmailResetPassword(emailOptions);

                // Assert
                // You can assert that the email sending logic is correct,
                // for example, by mocking SmtpClient and verifying its methods.
            }

            
            

            
        }
    
}