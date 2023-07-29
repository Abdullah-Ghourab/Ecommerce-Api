namespace KShop.Core.DTOs
{
    public class CartDto
    {
        public int? Id { get; set; }
        public string UserId { get; set; } = null!;
        public List<ProductCart> ProductCarts { get; set; } = null!;
    }
}
