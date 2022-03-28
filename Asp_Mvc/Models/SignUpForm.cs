using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc.Models
{
    public class SignUpForm
    {
        public SignUpForm()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
            AddressLine = "";
            PostalCode = "";
            City = "";
            ReturnUrl = "/";
            RoleName = "user";
        }

        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Du måste ange ett förnamn")]
        [StringLength(256, ErrorMessage = "Förnamnet måste bestå av minst 2 tecken", MinimumLength = 2)]
        [RegularExpression(@"^([a-öA-Ö]+?)([-][a-öA-Ö]+)*?$", ErrorMessage = "Must be a valid first name")]

        public string FirstName { get; set; } = "";

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Du måste ange ett efternamn")]
        [StringLength(256, ErrorMessage = "Efternamnet måste bestå av minst 2 tecken", MinimumLength = 2)]
        [RegularExpression(@"^([a-öA-Ö]+?)([-\s][a-öA-Ö]+)*?$", ErrorMessage = "Must be a valid last name")]
        public string LastName { get; set; } = "";

        [Display(Name = "E-postadress")]
        [Required(ErrorMessage = "Du måste ange en e-postadress")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Must contain a valid email address")]
        public string Email { get; set; } = "";

        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password must meet the minimum requirements")]
        public string Password { get; set; } = "";

        [Display(Name = "Bekräfta Lösenord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Du måste ange bekräfta lösenordet")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        public string ConfirmPassword { get; set; } = "";

        [Display(Name = "Gatuadress")]
        [Required(ErrorMessage = "Du måste ange en gatuadress")]
        [StringLength(256, ErrorMessage = "Gatuadressen måste bestå av minst 2 tecken", MinimumLength = 2)]
        [RegularExpression(@"^([a-öA-Ö]+?)([\s][0-9]+)*?$", ErrorMessage = "Must be a valid streetname")]

        public string AddressLine { get; set; } = "";

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Du måste ange ett postnummer")]
        [StringLength(256, ErrorMessage = "Postnumret måste bestå av minst 5 tecken", MinimumLength = 5)]
        [RegularExpression(@"^\d{3}(\s\d{2})?$", ErrorMessage = "Must be a valid postal code (eg. 123 45)")]

        public string PostalCode { get; set; } = "";

        [Display(Name = "Ort")]
        [Required(ErrorMessage = "Du måste ange en ort")]
        [StringLength(256, ErrorMessage = "Orten måste bestå av minst 2 tecken", MinimumLength = 2)]
        [RegularExpression(@"^([a-öA-Ö]+?)([\s][a-öA-Ö]+)*?$", ErrorMessage = "Must be a valid name")]

        public string City { get; set; } = "";
        public string ErrorMessage { get; set; } = "";

        public string ReturnUrl { get; set; } = "/";

        public string RoleName { get; set; } = "user";
        public string DisplayName => $"{FirstName} {LastName}";

    }
}

