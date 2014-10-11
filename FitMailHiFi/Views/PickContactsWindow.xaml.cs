using System.Windows;

namespace FitMailHiFi.Views
{
    public partial class PickContactsWindow
    {
        public PickContactsWindow()
        {
            InitializeComponent();
        }

        private void ConfirmClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
