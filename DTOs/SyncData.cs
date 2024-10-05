using SomeStoreAPI.Models;
using System.Collections;

namespace SomeStoreAPI.DTOs
{

    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ThumbnailImage { get; set; }
        public Price Price { get; set; }
        public Price OriginalPrice { get; set; }
        public Brand Brand { get; set; }
        public int Sold { get; set; }
        public bool AllowMultipleConfigs { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public double? ReviewScore { get; set; }
        public int? ReviewCount { get; set; }
        public bool Has3DAssets { get; set; }
        public Price FullPriceBeforeOverallDiscount { get; set; }
        public Price PossibleDiscountPrice { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Image> Images { get; set; }
        public Dictionary<int, List<ImageTaxonomyTag>> ImageTaxonomyTags { get; set; }
    }


    public class SyncData
    {
        //public int TotalPages { get; set; }

        //public int TotalItems { get; set; }

        //public int PageSize { get; set; }


        public IEnumerable<ProductResponse> Products { get; set; }

        //public IEnumerable FilterTags { get; set; }

        //public IEnumerable FilteredCategories { get; set; }
    }
}
