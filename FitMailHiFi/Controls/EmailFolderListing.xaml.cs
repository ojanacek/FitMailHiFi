using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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
            DependencyProperty.Register("Emails", typeof (ObservableCollection<EmailViewModel>), typeof (EmailFolderListing));
        public static readonly DependencyProperty SearchedExpressionProperty =
            DependencyProperty.Register("SearchedExpression", typeof (string), typeof (EmailFolderListing), new FrameworkPropertyMetadata(OnSearchedExpressionChanged));
        public static readonly DependencyProperty SelectedEmailProperty =
            DependencyProperty.Register("SelectedEmail", typeof(EmailViewModel), typeof(EmailFolderListing));
        
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

        public EmailViewModel SelectedEmail
        {
            get { return (EmailViewModel) GetValue(SelectedEmailProperty); }
            set { SetValue(SelectedEmailProperty, value); }
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
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                e.Handled = true;
                new EmailDetailsWindow { DataContext = SelectedEmail }.Show();
            }
        }
    }
}
