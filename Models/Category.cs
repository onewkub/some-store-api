using System.Text.Json.Serialization;

namespace SomeStoreAPI.Models
{
    public class Category
    {
        public int Id { get; set; }  // Primary key
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
