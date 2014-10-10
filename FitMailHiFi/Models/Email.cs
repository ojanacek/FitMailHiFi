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

        public Email()
        {
            
        }

        public Email(string fromAddress, List<string> toAddresses, string subject, string body, DateTime date, List<string> copyToAddresses = null, List<string> blindCopyToAddresses = null)
        {
            FromAddress = fromAddress;
            ToAddresses = toAddresses;
            CopyToAddresses = copyToAddresses;
            BlindCopyToAddresses = blindCopyToAddresses;
            Subject = subject;
            Body = body;
            Date = date;
        }
    }
}