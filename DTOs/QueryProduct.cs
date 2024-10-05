namespace SomeStoreAPI.DTOs
{
    public class QueryProduct
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }

        public string? OrderBy { get; set; } = "name";

        public string? OrderType { get; set; } = "asc";

        public string? Search { get; set; } = String.Empty;


    }

}
