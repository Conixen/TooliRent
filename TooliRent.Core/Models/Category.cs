using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        // navigation property
        public ICollection<Tool> Tools { get; set; }    // one category can have many tools
    }
}
