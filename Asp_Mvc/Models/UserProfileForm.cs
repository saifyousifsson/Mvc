namespace Asp_Mvc.Models
{
    public class UserProfileForm
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty ;
        public string UserId { get; set; } = string.Empty ;
        public string DisplayName => $"{FirstName} {LastName}";


    }
}
