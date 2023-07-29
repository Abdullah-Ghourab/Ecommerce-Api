namespace KShop.Core.Models

{
   
    public  class Category : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string? Image { get; set; }
        public string? ImageThumbnail { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
