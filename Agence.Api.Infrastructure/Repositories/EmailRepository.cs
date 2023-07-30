using System.Net;
using Agence.Api.Application.Repositories;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Agence.Api.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
	{
        private readonly SendGridClient _sendGridClient;
        private readonly string _fromEmail;

        public EmailRepository(SendGridClient sendGridClient, IOptions<SendGridOptions> sendGridOptions)
        {
            _sendGridClient = sendGridClient;
            _fromEmail = "appointment@micasa.be";
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var from = new EmailAddress(_fromEmail);
            var to = new EmailAddress(toEmail);
            var email = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            var response = await _sendGridClient.SendEmailAsync(email);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email: {response.Body}");
            }
        }
    }
}

