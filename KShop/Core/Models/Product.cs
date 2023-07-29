using System.ComponentModel.DataAnnotations.Schema;

namespace KShop.Core.Models
{
    public class Product :BaseModel

    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

      
        public decimal Price { get; set; }

        public string Description { get; set; } = null!;
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public List<ProductImage> ProductImages { get; set; } = null!;
        public List<ProductThumbnailImage> ProductThumbnailImages { get; set; } = null!;
        public ICollection<CartProduct>? CartProducts { get; set; }

    }
}
