using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities
{
    public class MailSettingsModel
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty; 

        public string Body { get; set; } = string.Empty; 

        public string To { get; set; } = string.Empty;

        IList<IFormFile>? Attachments { get; set; }
    }
}
