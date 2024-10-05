namespace SomeStoreAPI.DTOs
{
    public class PaginationData<T>
    {
        public IEnumerable<T> Data {  get; set; }

        public int Count {  get; set; }
    }
}
