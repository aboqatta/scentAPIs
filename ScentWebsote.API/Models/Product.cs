using System.ComponentModel.DataAnnotations;

namespace ScentWebsote.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        // Foreign key for Category
        public int CategoryId { get; set; }

        // Navigation property
       public Category Category { get; set; }
    }
}
