using System.Security;

namespace WebApp.Configuration
{
    public class SmtpClientSettings
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Pwd { get; set; }
    }
}
