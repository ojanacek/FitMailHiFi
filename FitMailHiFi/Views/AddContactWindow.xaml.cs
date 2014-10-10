using FitMailHiFi.ViewModels;

namespace FitMailHiFi.Views
{
    public partial class AddContactWindow
    {
        public AddContactWindow()
        {
            InitializeComponent();
            DataContext = new AddContactViewModel(this);
        }
    }
}
