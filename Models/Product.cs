namespace SomeStoreAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }  // This is the primary key
        public string Name { get; set; }
        public string Title { get; set; }
        public string ThumbnailImage { get; set; }

        public Price Price { get; set; }  // Complex type
        public Price OriginalPrice { get; set; }  // Complex type

        public Brand Brand { get; set; }  // Complex type

        public int Sold { get; set; }
        public bool AllowMultipleConfigs { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }

        public double? ReviewScore { get; set; }
        public int? ReviewCount { get; set; }

        public Price FullPriceBeforeOverallDiscount { get; set; }
        public Price PossibleDiscountPrice { get; set; }

        // Foreign key relationships
        public ICollection<Category> Categories { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Image> Images { get; set; }

        //public Dictionary<int, List<ImageTaxonomyTag>> ImageTaxonomyTags { get; set; }
    }
}
