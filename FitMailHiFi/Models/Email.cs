using System;
using System.Collections.Generic;

namespace FitMailHiFi.Models
{
    public class Email
    {
        public string FromAddress { get; set; }
        public List<string> ToAddresses { get; set; }
        public List<string> CopyToAddresses { get; set; }
        public List<string> BlindCopyToAddresses { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}