using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FitMailHiFi.Models;
using FitMailHiFi.ViewModels;
using FitMailHiFi.Views;

namespace FitMailHiFi.Controls
{
    public partial class NewEmail
    {
        public NewEmail()
        {
            InitializeComponent();
        }

        private void NewEmail_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue == false)
                return;

            var mail = MainController.Instance.RespForwEmail;
            if (mail == null)
                return;

            if (mail.ToAddresses != null)
                To.Text = mail.ToAddresses[0];

            Subject.Text = mail.Subject;
            Body.Text = mail.Body;

            MainController.Instance.RespForwEmail = null;
        }

        private void OpenContactsForReceiver(object sender, RoutedEventArgs e)
        {
            PickAndFillContacts(To);
        }

        private void OpenContactsForCopy(object sender, RoutedEventArgs e)
        {
            PickAndFillContacts(Copy);
        }

        private void OpenContactsForBlindCopy(object sender, RoutedEventArgs e)
        {
            PickAndFillContacts(BlindCopy);
        }

        private void PickAndFillContacts(TextBox contactsBox)
        {
            var window = new PickContactsWindow { DataContext = this.DataContext };
            var result = window.ShowDialog();

            var pickedContacts = FindSelectedContacts();

            if (result != null && !result.Value)
            {
                DeselectContacts(pickedContacts);
                return;
            }
            
            FillContacts(pickedContacts.Select(cvm => cvm.Contact), contactsBox);
            DeselectContacts(pickedContacts);
        }

        private IEnumerable<ContactViewModel> FindSelectedContacts()
        {
            var viewModel = DataContext as MainViewModel;
            return viewModel.Contacts.Where(c => c.IsSelected);
        }

        private void FillContacts(IEnumerable<Contact> contacts, TextBox targetBox)
        {
            var joinedContacts = string.Join(",", contacts.Select(c => c.EmailAddress));
            targetBox.Text = joinedContacts;
        }

        private void DeselectContacts(IEnumerable<ContactViewModel> contacts)
        {
            foreach (var contact in contacts)
            {
                contact.IsSelected = false;
            }
        }

        private void SendEmail(object sender, RoutedEventArgs e)
        {
            var toAddresses = To.Text.Split(',').ToList();
            var copyToAddresses = Copy.Text.Split(',').ToList();
            var blindCopyToAddresses = BlindCopy.Text.Split(',').ToList();

            MessageBox.Show("Email byl odeslán.", "Informace", MessageBoxButton.OK, MessageBoxImage.Information);
            MainController.Instance.SendEmail(toAddresses, Subject.Text, Body.Text, copyToAddresses, blindCopyToAddresses);
            To.Clear();
            Copy.Clear();
            BlindCopy.Clear();
            Subject.Clear();
            Body.Clear();
        }
    }
}
