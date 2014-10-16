using System.Linq;
using System.Windows.Input;
using FitMailHiFi.ViewModels;

namespace FitMailHiFi.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewEmailShortcut(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel.WriteNewEmail.Execute(null);
        }

        private void ShowHelpShortcut(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel.ShowHelp.Execute(null);
        }

        private void SaveAllContacts(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            foreach (var contact in viewModel.Contacts.Where(c => c.IsBeingEdited))
            {
                contact.Save.Execute(null);
            }
        }
    }
}
