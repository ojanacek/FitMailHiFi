using System.Windows;
using System.Windows.Input;
using EveryDayTools.WPF;

namespace FitMailHiFi.ViewModels
{
    public class AddContactViewModel : ViewModelBase
    {
        private readonly Window boundWindow;

        private string email;
        public string Email
        {
            get { return email; }
            set { SetField(ref email, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value); }
        }

        public ICommand AddContact { get { return new RelayCommand(AddNewContact); } }

        public AddContactViewModel(Window window)
        {
            boundWindow = window;
        }

        private void AddNewContact()
        {
            MainController.Instance.AddContact(Email, Name);
            boundWindow.Close();
        }
    }
}