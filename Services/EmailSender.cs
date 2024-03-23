using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace COMP2139_Lab1.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _mailGunKey;

        public EmailSender(IConfiguration configuration)
        {
            _mailGunKey = configuration["MailGun:ApiKey"];
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           var client = new SendGridClient(_mailGunKey);
           var from = new EmailAddress("teimur.terchyyev@georgebrown.ca", "Project Collaborator");
           var to = new EmailAddress(email);
           var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
           await client.SendEmailAsync(msg);
        }
    }
}
