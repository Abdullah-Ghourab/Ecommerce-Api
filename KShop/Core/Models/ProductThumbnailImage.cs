namespace KShop.Core.Models
{
    public class ProductThumbnailImage
    {
        public int Id { get; set; }
        public string ImageThumbnail { get; set; } = null!;
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
