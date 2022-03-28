namespace Asp_Mvc.Models
{
    public class EditProfileForm
    {
        public string UserId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public string StreetName { get; set; } 
        public string PostalCode { get; set; }
        public string City { get; set; } 
        public string Country { get; set; }
    }
}
