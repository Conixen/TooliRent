using System.ComponentModel.DataAnnotations;

namespace TooliRent.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<Tool> Tools { get; set; } = new List<Tool>(); // one category can have many tools
    }
}
