using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_Mvc.Data
{
    public class ImageEntity
    {
      
        [Key]
        public string FileName { get; set; }

        [NotMapped]
        [Display(Name = "Upload File")]
        public IFormFile File { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

    }
}
