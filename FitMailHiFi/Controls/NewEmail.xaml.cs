using System.Linq;
using System.Windows;

namespace FitMailHiFi.Controls
{
    public partial class NewEmail
    {
        public NewEmail()
        {
            InitializeComponent();
        }

        private void OpenContacts(object sender, RoutedEventArgs e)
        {
            
        }

        private void SendEmail(object sender, RoutedEventArgs e)
        {
            var toAddresses = To.Text.Split(',').ToList();
            var copyToAddresses = Copy.Text.Split(',').ToList();
            var blindCopyToAddresses = BlindCopy.Text.Split(',').ToList();

            MainController.Instance.SendEmail(toAddresses, Subject.Text, Body.Text, copyToAddresses, blindCopyToAddresses);

            MessageBox.Show("Email byl odeslán.");
        }
    }
}
