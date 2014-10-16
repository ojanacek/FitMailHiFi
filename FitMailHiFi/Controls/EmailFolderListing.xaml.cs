using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using EveryDayTools;
using FitMailHiFi.ViewModels;
using FitMailHiFi.Views;

namespace FitMailHiFi.Controls
{
    public partial class EmailFolderListing
    {
        private static ICollectionView emailsView;
        
        #region Dependency properties

        public static readonly DependencyProperty EmailsProperty =
            DependencyProperty.Register("Emails", typeof(ObservableCollection<EmailViewModel>), typeof(EmailFolderListing));
        public static readonly DependencyProperty SearchedExpressionProperty =
            DependencyProperty.Register("SearchedExpression", typeof (string), typeof (EmailFolderListing), new FrameworkPropertyMetadata(OnSearchedExpressionChanged));
        
        public ObservableCollection<EmailViewModel> Emails
        {
            get { return (ObservableCollection<EmailViewModel>) GetValue(EmailsProperty); }
            set { SetValue(EmailsProperty, value); }
        }

        public string SearchedExpression
        {
            get { return (string) GetValue(SearchedExpressionProperty); }
            set { SetValue(SearchedExpressionProperty, value); }
        }

        #endregion

        public EmailFolderListing()
        {
            InitializeComponent();
        }

        private void EmailFolderListing_OnLoaded(object sender, RoutedEventArgs e)
        {
            emailsView = CollectionViewSource.GetDefaultView(Emails);
            emailsView.Filter = FilterEmails;
            emailsView.SortDescriptions.Add(new SortDescription("Email.Date", ListSortDirection.Descending));
        }

        private bool FilterEmails(object item)
        {
            if (string.IsNullOrWhiteSpace(SearchedExpression))
                return true;

            var email = item as EmailViewModel;
            return email.Email.Subject.ContainsIgnoreCase(SearchedExpression) || email.Email.FromAddress.ContainsIgnoreCase(SearchedExpression);
        }

        private static void OnSearchedExpressionChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            emailsView.Refresh();
        }

        private void MailDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var list = sender as ListView;
            new EmailDetailsWindow { DataContext = list.SelectedItems[0] }.Show();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            DeleteEmails();
        }

        private void EmailsList_OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.Delete)
                DeleteEmails();
        }

        private void DeleteEmails()
        {
            var result = MessageBox.Show("Opravdy si přejete smazat vybrané emaily?", "Potvrzení akce", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.No)
                return;

            var selectedItems = EmailsList.SelectedItems.Cast<EmailViewModel>().ToList();
            foreach (var mail in selectedItems)
            {
                MainController.Instance.DeleteEmail(mail.Email);
            }
        }
    }
}
