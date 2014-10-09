using EveryDayTools.WPF;
using FitMailHiFi.Models;

namespace FitMailHiFi.ViewModels
{
    public class EmailViewModel : ViewModelBase
    {
        public Email Email { get; set; }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { SetField(ref isChecked, value); }
        }

        public EmailViewModel(Email email)
        {
            Email = email;
        }
    }
}