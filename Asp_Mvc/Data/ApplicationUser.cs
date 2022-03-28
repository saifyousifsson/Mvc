using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        public string LastName { get; set; }
    }
}
