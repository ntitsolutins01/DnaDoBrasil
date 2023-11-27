using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
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
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Teste", "fmuniz0001@gmail.com"));
                message.To.Add(MailboxAddress.Parse("fmuniz0001@gmail.com"));
                message.Subject = "teste";
                message.Body = new TextPart("plain")
                {
                    Text = @"Hello Fabio"
                };
                var mai = "no_replay@idecace.org.br";
                var senha = "Idecace@2023";

                var client = new SmtpClient();

                try
                {
                    client.Connect("email-ssl.com.br", 465, true);
                    client.Authenticate(mai, senha);
                    client.Send(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }


                //var apiKey = "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";
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("test@example.com", "Example User");
                //var subject = "Sending with SendGrid is Fun";
                //var to = new EmailAddress("test@example.com", "Example User");
                //var plainTextContent = "and easy to do anywhere with C#.";
                //var htmlContent = "<strong>and easy to do anywhere with C#.</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);


                //var apiKey = _settings.ApiKey;

                //var myMessage = new SendGridMessage();
                //myMessage.AddTo(email);
                //myMessage.From = new EmailAddress(_settings.FromEmail, _settings.FromName);
                //myMessage.Subject = subject;
                //myMessage.HtmlContent = htmlMessage;

                //var transportWeb = new SendGridClient(apiKey);
                //var response = await transportWeb.SendEmailAsync(myMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
