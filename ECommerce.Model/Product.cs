using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Model
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public string? ImageUrl { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
    }
}
