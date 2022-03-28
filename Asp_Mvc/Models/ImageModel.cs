using System.ComponentModel.DataAnnotations;

namespace Asp_Mvc.Models
{
    public class ImageModel
    {
    
        [Required]
        [Display(Name = "Upload File")]
        public IFormFile File { get; set; }
        
    }
}
