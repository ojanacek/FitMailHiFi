using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using EveryDayTools;
using FitMailHiFi.ViewModels;

namespace FitMailHiFi.Controls
{
    public partial class ContactList
    {
        private static ICollectionView contactsView;

        #region Dependency properties

        public static readonly DependencyProperty ContactsProperty =
            DependencyProperty.Register("Contacts", typeof(ObservableCollection<ContactViewModel>), typeof(ContactList));
        public static readonly DependencyProperty SearchedExpressionProperty =
            DependencyProperty.Register("SearchedExpression", typeof(string), typeof(ContactList), new FrameworkPropertyMetadata(OnSearchedExpressionChanged));

        public ObservableCollection<ContactViewModel> Contacts
        {
            get { return (ObservableCollection<ContactViewModel>) GetValue(ContactsProperty); }
            set { SetValue(ContactsProperty, value); }
        }

        public string SearchedExpression
        {
            get { return (string)GetValue(SearchedExpressionProperty); }
            set { SetValue(SearchedExpressionProperty, value); }
        }

        #endregion

        public ContactList()
        {
            InitializeComponent();
        }

        private void ContactList_OnLoaded(object sender, RoutedEventArgs e)
        {
            contactsView = CollectionViewSource.GetDefaultView(Contacts);
            contactsView.Filter = FilterContacts;
            contactsView.SortDescriptions.Add(new SortDescription("Contact.FullName", ListSortDirection.Ascending));
        }

        private bool FilterContacts(object item)
        {
            if (string.IsNullOrWhiteSpace(SearchedExpression))
                return true;

            var contact = item as ContactViewModel;
            return contact.Contact.EmailAddress.ContainsIgnoreCase(SearchedExpression) || contact.Contact.FullName.ContainsIgnoreCase(SearchedExpression);
        }

        private static void OnSearchedExpressionChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            contactsView.Refresh();
        }
    }
}
