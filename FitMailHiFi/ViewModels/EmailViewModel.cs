using EveryDayTools.WPF;
using FitMailHiFi.Models;

namespace FitMailHiFi.ViewModels
{
    public class EmailViewModel : ViewModelBase
    {
        public Email Email { get; set; }

        public EmailViewModel(Email email)
        {
            Email = email;
        }
    }
}