using System;
using System.Collections.Generic;
using FitMailHiFi.Models;

namespace FitMailHiFi
{
    public class MainController
    {
        private static readonly MainController instance = new MainController();
        public static MainController Instance { get { return instance; } }
        private MainController() { }

        private readonly List<Email> receivedEmails = new List<Email>();
        private readonly List<Email> sentEmails = new List<Email>();
        private readonly List<Email> deletedEmails = new List<Email>();
        private readonly List<Contact> contacts = new List<Contact>();

        public List<Email> ReceivedEmails { get { return receivedEmails; } }
        public List<Email> SentEmails { get { return sentEmails; } }
        public List<Email> DeletedEmails { get { return deletedEmails; } }
        public List<Contact> Contacts { get { return contacts; } }

        public event EventHandler<Contact> NewContactAdded;
        public event EventHandler NewEmailSent;
        public event EventHandler<Email> EmailDeleted;

        public event EventHandler RespForwRequested;
        public Email RespForwEmail { get; set; }

        public void AddContact(string emailAddress, string fullName)
        {
            var contact = new Contact(emailAddress, fullName);
            contacts.Add(contact);
            NewContactAdded(this, contact);
        }

        public void SendEmail(List<string> toAddresses, string subject, string body, List<string> copyToAddresses = null, List<string> blindCopyToAddresses = null)
        {
            var email = new Email("me@mail.com", toAddresses, subject, body, DateTime.Now, copyToAddresses, blindCopyToAddresses);
            sentEmails.Add(email);
            NewEmailSent(this, EventArgs.Empty);
        }

        public void DeleteEmail(Email email)
        {
            if (receivedEmails.Contains(email))
                receivedEmails.Remove(email);

            if (sentEmails.Contains(email))
                sentEmails.Remove(email);

            deletedEmails.Add(email);

            EmailDeleted(this, email);
        }

        public void RequestRespForw()
        {
            RespForwRequested(this, EventArgs.Empty);
        }
    }
}