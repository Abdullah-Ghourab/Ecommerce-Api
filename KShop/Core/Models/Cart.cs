using System.ComponentModel.DataAnnotations.Schema;

namespace KShop.Core.Models
{
    public class Cart : BaseModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = null!;
        public ApplicationUser? User { get; set; }
        public ICollection<CartProduct>? CartProducts { get; set; }
    }
}
