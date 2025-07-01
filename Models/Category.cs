using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace firs_dot_net_project.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category name")]
        public string Name { get; set; }
        [DisplayName("Category order")]
        [Range(1, 1000, ErrorMessage = "Display order must be between 1 and 1000")]
        public int DisplayOrder { get; set; }

    }
}
