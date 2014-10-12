using System.Windows;
using System.Windows.Input;
using EveryDayTools.WPF;
using FitMailHiFi.Models;

namespace FitMailHiFi.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        private Contact contact;
        public Contact Contact
        {
            get { return contact; }
            set { SetField(ref contact, value); }
        }

        private Contact editedContact;
        public Contact EditedContact
        {
            get { return editedContact; }
            set { SetField(ref editedContact, value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetField(ref isSelected, value); }
        }

        private bool isDeleted;
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { SetField(ref isDeleted, value); }
        }

        private bool isBeingEdited;
        public bool IsBeingEdited
        {
            get { return isBeingEdited; }
            set { SetField(ref isBeingEdited, value); }
        }

        private bool isNotBeingEdited = true;
        public bool IsNotBeingEdited
        {
            get { return isNotBeingEdited; }
            set { SetField(ref isNotBeingEdited, value); }
        }

        public ICommand Cancel { get { return new RelayCommand(CancelChanges); } }
        public ICommand Delete { get { return new RelayCommand(DeleteContact); } }
        public ICommand Edit 
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsBeingEdited = true;
                    IsNotBeingEdited = false;
                });
            } 
        } 

        public ICommand Save { get { return new RelayCommand(SaveChanges); } }

        public ContactViewModel(Contact contact)
        {
            Contact = contact;
            EditedContact = new Contact(contact.EmailAddress, contact.FullName);
        }

        private void DeleteContact()
        {
            var result = MessageBox.Show("Opravdu si přejete odstranit kontakt?", "Smazat kontakt", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
                IsDeleted = true;
        }

        private void SaveChanges()
        {
            Contact = new Contact(EditedContact.EmailAddress, EditedContact.FullName);
            IsBeingEdited = false;
            IsNotBeingEdited = true;
        }

        private void CancelChanges()
        {
            EditedContact = new Contact(Contact.EmailAddress, Contact.FullName);
            IsBeingEdited = false;
            IsNotBeingEdited = true;
        }
    }
}