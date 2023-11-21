using System;
using System.Collections.Generic;
using System.Text;

namespace Infraero.Relprev.CrossCutting.Configuration
{
    public class SmtpClientSettings
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }


    }
}
