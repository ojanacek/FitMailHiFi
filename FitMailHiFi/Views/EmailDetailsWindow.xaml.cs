using System;
using System.Collections.Generic;
using System.Windows;
using FitMailHiFi.Models;
using FitMailHiFi.ViewModels;

namespace FitMailHiFi.Views
{
    public partial class EmailDetailsWindow
    {
        public EmailDetailsWindow()
        {
            InitializeComponent();
        }

        private void DeleteEmailClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Opravdu chcete smazat tento email?", "Smazat email", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            var mailViewModel = DataContext as EmailViewModel;
            MainController.Instance.DeleteEmail(mailViewModel.Email);
            Close();
        }

        private void RespondClick(object sender, RoutedEventArgs e)
        {
            var mailViewModel = DataContext as EmailViewModel;

            MainController.Instance.RespForwEmail = new Email
            {
                ToAddresses = new List<string> { mailViewModel.Email.FromAddress },
                Subject = "re: " + mailViewModel.Email.Subject,
                Body = "\n\n" + new string('-', 15) + "\n" + mailViewModel.Email.Body
            };
            MainController.Instance.RequestRespForw();
            Close();
        }

        private void ForwardClick(object sender, RoutedEventArgs e)
        {
            var mailViewModel = DataContext as EmailViewModel;

            MainController.Instance.RespForwEmail = new Email
            {
                Subject = "fw: " + mailViewModel.Email.Subject,
                Body = mailViewModel.Email.Body
            };
            MainController.Instance.RequestRespForw();
            Close();
        }
    }
}
