using Mango.Services.EmailAPI.Data;
using Mango.Services.EmailAPI.Message;
using Mango.Services.EmailAPI.Models;
using Mango.Services.EmailAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Azure.Communication.Email;
using Azure;
using ExtensionMethods;

namespace Mango.Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDBContext> _dbOptions;

        public EmailService(DbContextOptions<AppDBContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
           StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Email Requested");
            message.AppendLine("<br/>Total " + cartDto.CartHeader.CartTotal);
            message.AppendLine("<br/>");
            message.AppendLine("<ul>");
            foreach(var item in cartDto.CartDetails)
            {
                message.AppendLine("<li>");
                message.AppendLine(item.Product.Name + " x " + item.Count);
                message.AppendLine("<li/");
            }

            message.AppendLine("<ul/>");

            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        public async Task LogOrderPlaced(RewardMessage rewardsDto)
        {
            string message = "New Order placed. <br/> Order ID: " + rewardsDto.OrderId;
            await LogAndEmail(message, "test@test.com");
        }

        public async Task RegisterUserEmailAndLog(string email)
        {
            string message = "User Registration Successful. <br/> email: " + email;
            await LogAndEmail(message, "test@test.com");
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };

                await using var _db = new AppDBContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();

                var result = SendEmail(message, email);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email operation failed. {ex.Message}");
                return false;
            }
        }

        private static bool SendEmail(string message, string email)
        {
            // This code retrieves your connection string from an environment variable.
            string connectionString = "endpoint=https://communication-service-email-demo-sck.unitedstates.communication.azure.com/;accesskey=H4BWfMNcuBWKoey9KQHT2Iep4zoc7eP1qakYrCfPKuFeiGPYS10yc4uX/7yonqLcj+lmY2bIB+uVe2T6HMu42A==";
            var emailClient = new EmailClient(connectionString);

            // Create email content
            var emailContent = new EmailContent("This is a test Subject")
            {
                PlainText = message.ConvertToPlainText(),
                Html = message
            };

            var toRecipients = new List<EmailAddress>
            {
                new EmailAddress(
                    address: email,
                    displayName: "Work"),
                new EmailAddress(
                    address: "stevek357@hotmail.com",
                    displayName: "Steve1")
            };

            var ccRecipients = new List<EmailAddress>
            {
                new EmailAddress(
                    address: "stevek92@gmail.com",
                    displayName: "Steve2")
            };

            var emailRecipients = new EmailRecipients(toRecipients, ccRecipients);

            // create email message
            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@59aac6ae-62a8-469d-8b35-a75b89957797.azurecomm.net",
                emailRecipients,
                content: emailContent
                );


            try
            {
                var emailSendOperation = emailClient.Send(
                    wait: WaitUntil.Completed,
                    message: emailMessage);

                Console.WriteLine($"Email sent.  Status = {emailSendOperation.Value.Status}");

                string operationId = emailSendOperation.Id;
                Console.WriteLine($"Email Op Id: {operationId}");


                return true;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Email send operation failed.  Error Code: {ex.ErrorCode}, {ex.Message}");
                return false;
            }

        }
    }
}
