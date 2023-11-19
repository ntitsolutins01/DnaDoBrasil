using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using WebApp.Configuration;
using SendGrid.Helpers.Mail;

namespace WebApp.Services
{
    public class SendGridEmailService : IEmailSender
    {
        private readonly SendGridSettings _settings;

        public SendGridEmailService(IOptions<SendGridSettings> settings)
        {
            _settings = settings.Value;
        }

        //metodo para teste de envio de email local usando apisendgrid
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = _settings.ApiKey;

            var myMessage = new SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new EmailAddress(_settings.FromEmail, _settings.FromName);
            myMessage.Subject = subject;
            myMessage.HtmlContent = htmlMessage;

            var transportWeb = new SendGridClient(apiKey);
            await transportWeb.SendEmailAsync(myMessage);
        }

        //metodo para teste de envio de email ambiente dna
        //public async Task SendEmailAsync(string toAddress, string subject, string message)
        //{
        //    using (var client = new System.Net.Mail.SmtpClient(_settings.Host, Convert.ToInt32(_settings.Port)))
        //    using (var mailMessage = new MailMessage(_settings.FromEmail, toAddress, subject, message))
        //    {
        //        //client.Credentials = new NetworkCredential(configuration["Email:Username"], configuration["Email:Password"]);
        //        //client.EnableSsl = true;

        //        mailMessage.IsBodyHtml = true;

        //        try
        //        {
        //            await client.SendMailAsync(mailMessage);
        //        }
        //        catch (SmtpException)
        //        {
        //            throw;
        //        }
        //    }
        //}
    }
}
