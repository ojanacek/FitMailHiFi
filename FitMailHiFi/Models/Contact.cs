namespace FitMailHiFi.Models
{
    public class Contact
    {
        public string EmailAddress { get; set; }
        public string FullName { get; set; }

        public Contact()
        {
            
        }

        public Contact(string emailAddress, string fullName)
        {
            EmailAddress = emailAddress;
            FullName = fullName;
        }
    }
}