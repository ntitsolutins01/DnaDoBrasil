namespace WebApp.Configuration
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
