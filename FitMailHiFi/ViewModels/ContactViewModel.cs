using FitMailHiFi.Models;

namespace FitMailHiFi.ViewModels
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }

        public ContactViewModel(Contact contact)
        {
            Contact = contact;
        }
    }
}