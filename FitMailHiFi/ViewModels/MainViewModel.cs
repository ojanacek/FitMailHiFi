using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EveryDayTools;
using EveryDayTools.WPF;
using FitMailHiFi.Models;
using FitMailHiFi.Views;

namespace FitMailHiFi.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainController controller = MainController.Instance;

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

        public ICommand WriteNewEmail { get { return new RelayCommand(ChangeContentToNewEmail); } }
        public ICommand ShowContacts { get { return new RelayCommand(ChangeContentToContacts); } }

        public ICommand ShowReceived
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ChangeContentToEmailListing();
                    SwitchActiveEmails(controller.ReceivedEmails);
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
                    SwitchActiveEmails(controller.SentEmails);
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
                    SwitchActiveEmails(controller.DeletedEmails);
                    IsDeleteAvailable = false;
                });
            }
        }
        
        public ICommand DeleteSelectedEmails { get { return new RelayCommand(DeleteEmails); } }
        public ICommand AddContact { get { return new RelayCommand(AddNewContact); } }

        #endregion

        private bool isDeleteAvailable;
        public bool IsDeleteAvailable
        {
            get { return isDeleteAvailable; }
            set { SetField(ref isDeleteAvailable, value); }
        }

        #endregion

        public MainViewModel()
        {
            activeFolderEmails = new ObservableCollection<EmailViewModel>();
            contacts = new ObservableCollection<ContactViewModel>();

            controller.NewContactAdded += (s, a) => Contacts.Add(new ContactViewModel(a));
            controller.NewEmailSent += (s, a) => ShowReceived.Execute(null);

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
                controller.ReceivedEmails.Add(email);
            }
            
            controller.AddContact("jan.novak@novak.cz", "Jan Novák");
            controller.AddContact("pepa.novak@novak.cz", "Pepa Novák");
            controller.AddContact("zbynek.dlouhy@gmail.com", "Zbyněk Dlouhý");
            controller.AddContact("joskaxx@posta.cz", "Josef Malý");
            controller.AddContact("ladan@posta.cz", "Ladislav Polívka");
            controller.AddContact("cervond@yahoo.com", "Ondřej Červinka");
            controller.AddContact("eva.sladka@gmail.com", "Eva Sladká");
            controller.AddContact("kratter@seznam.cz", "Tereza Krátká");
            controller.AddContact("janica@email.cz", "Jana Ostrá");
        }

        private void DeleteEmails()
        {
            var result = MessageBox.Show("Opravdy si přejete smazat vybrané emaily?", "Potvrzení akce", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            var mailsToDelete = ActiveFolderEmails.Where(e => e.IsChecked);
            foreach (var mail in mailsToDelete.ToArray())
            {
                var mailModel = controller.ReceivedEmails.Single(e => e == mail.Email);
                controller.ReceivedEmails.Remove(mailModel);
                ActiveFolderEmails.Remove(mail);
                controller.DeletedEmails.Add(mailModel);
            }
        }

        private void AddNewContact()
        {
            new AddContactWindow().ShowDialog();
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

        private void ChangeContentToNewEmail()
        {
            AreContactsActive = false;
            IsEmailDetailActive = false;
            IsEmailsListingActive = false;
            IsNewEmailActive = true;
        }

        private void SwitchActiveEmails(IEnumerable<Email> emails)
        {
            ActiveFolderEmails.Clear();
            ActiveFolderEmails.AddRange(emails.Select(e => new EmailViewModel(e)));
            IsDeleteAvailable = ActiveFolderEmails.Count > 0;
        }
    }
}