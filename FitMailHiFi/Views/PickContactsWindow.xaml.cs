using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FitMailHiFi.ViewModels;

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
            var context = DataContext as MainViewModel;
            if (context.Contacts.All(c => !c.IsSelected))
            {
                MessageBox.Show("Není vybrán žádný kontakt. Zaškrtněte prosím vybrané kotakty.");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ContactMouseClick(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            var selectedContact = grid.DataContext as ContactViewModel;
            selectedContact.IsSelected = !selectedContact.IsSelected;
        }
    }
}
