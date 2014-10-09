using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EveryDayTools;
using EveryDayTools.WPF;
using FitMailHiFi.Models;

namespace FitMailHiFi.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<Email> receivedEmails = new List<Email>();
        private readonly List<Email> sentEmails = new List<Email>();
        private readonly List<Email> deletedEmails = new List<Email>();

        #region Bounded properties

        #region Data collections

        private readonly ObservableCollection<EmailViewModel> activeFolderEmails;
        public ObservableCollection<EmailViewModel> ActiveFolderEmails { get { return activeFolderEmails; } }

        private readonly ObservableCollection<ContactViewModel> contacts;
        public ObservableCollection<ContactViewModel> Contacts { get { return contacts; } }

        #endregion

        #region Window state

        private bool isEmailsListingActive;
        public bool IsEmailsListingActive
        {
            get { return isEmailsListingActive; }
            set { SetField(ref isEmailsListingActive, value); }
        }

        private bool areContactsActive;
        public bool AreContactsActive
        {
            get { return areContactsActive; }
            set { SetField(ref areContactsActive, value); }
        }

        private bool isNewEmailActive;
        public bool IsNewEmailActive
        {
            get { return isNewEmailActive; }
            set { SetField(ref isNewEmailActive, value); }
        }

        private bool isEmailDetailActive;
        public bool IsEmailDetailActive
        {
            get { return isEmailDetailActive; }
            set { SetField(ref isEmailDetailActive, value); }
        }

        #endregion

        #region Commands

        public ICommand WriteNewEmail { get { return new RelayCommand(ChangeContentToContacts); } }
        public ICommand ShowContacts { get { return new RelayCommand(ChangeContentToContacts); } }

        public ICommand ShowReceived
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ChangeContentToEmailListing();
                    SwitchActiveEmails(receivedEmails);
                });
            }
        }

        public ICommand ShowSent
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ChangeContentToEmailListing();
                    SwitchActiveEmails(sentEmails);
                });
            }
        }

        public ICommand ShowTrash
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ChangeContentToEmailListing();
                    SwitchActiveEmails(deletedEmails);
                });
            }
        }
        
        public ICommand DeleteSelectedEmails { get { return new RelayCommand(DeleteEmails); } }
        public ICommand AddContact { get { return new RelayCommand(AddNewContact); } }

        #endregion

        #endregion

        public MainViewModel()
        {
            activeFolderEmails = new ObservableCollection<EmailViewModel>();
            contacts = new ObservableCollection<ContactViewModel>();
            PopulateWithTestData();
            ShowReceived.Execute(null);
        }

        private void PopulateWithTestData()
        {
            for (int i = 1; i < 10; i++)
            {
                var email = new Email
                {
                    FromAddress = "user" + i + "@mail.com",
                    ToAddresses = new List<string> { "me@mail.com" },
                    Subject = "Test message " + i,
                    Body = "Hi there, \n this is a test message from user " + i,
                    Date = DateTime.Parse("2014-0" + i + "-10")
                };
                receivedEmails.Add(email);
            }

            for (int i = 1; i < 10; i++)
            {
                var contact = new Contact
                {
                    EmailAddress = "user" + i + "@mail.com",
                    FullName = "Email User" + i
                };
                contacts.Add(new ContactViewModel(contact));
            }
        }

        private void DeleteEmails()
        {
            var mailsToDelete = ActiveFolderEmails.Where(e => e.IsChecked);
            foreach (var mail in mailsToDelete.ToArray())
            {
                var mailModel = receivedEmails.Single(e => e == mail.Email);
                receivedEmails.Remove(mailModel);
                ActiveFolderEmails.Remove(mail);
            }
        }

        private void AddNewContact()
        {
            
        }

        private void ChangeContentToContacts()
        {
            AreContactsActive = true;
            IsEmailDetailActive = false;
            IsEmailsListingActive = false;
            IsNewEmailActive = false;
        }

        private void ChangeContentToEmailListing()
        {
            AreContactsActive = false;
            IsEmailDetailActive = false;
            IsEmailsListingActive = true;
            IsNewEmailActive = false;
        }

        private void SwitchActiveEmails(IEnumerable<Email> emails)
        {
            ActiveFolderEmails.Clear();
            ActiveFolderEmails.AddRange(emails.Select(e => new EmailViewModel(e)));
        }
    }
}