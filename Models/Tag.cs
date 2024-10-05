using System.Text.Json.Serialization;

namespace SomeStoreAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }  // Primary key
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string UserIdentifier { get; set; }
        public string CollectionName { get; set; }
        public int CollectionId { get; set; }
        public string ThumbnailImage { get; set; }

        // Foreign key back to Product
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
