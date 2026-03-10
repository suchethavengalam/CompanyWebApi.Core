using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Core.DataAccess.Entities
{
    public class EmailSettings
    {
        public string Host { get; set; } = string.Empty;

        public int Port { get; set; }

        public string SenderEmail { get; set; } = string.Empty;

        public string SenderName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string TemplatePath { get; set; } = string.Empty;

        public string NotificationEmail { get; set; } = string.Empty;
    }
}
