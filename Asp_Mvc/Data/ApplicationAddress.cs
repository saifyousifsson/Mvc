using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc.Data
{
    public class ApplicationAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [PersonalData]
        public string AddressLine { get; set; }

        [Required]
        [PersonalData]
        public string PostalCode { get; set; }

        [Required]
        [PersonalData]
        public string City { get; set; }

        [Required]
        [PersonalData]
        public string Country { get; set; }
    }
}
