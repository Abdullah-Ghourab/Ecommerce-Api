using KShop.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KShop.Core.DTOs
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}
