using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using EveryDayTools;
using FitMailHiFi.ViewModels;

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
    }
}
