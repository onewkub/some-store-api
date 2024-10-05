using SomeStoreAPI.DTOs;
using SomeStoreAPI.Models;

namespace SomeStoreAPI.Services
{
    public interface IProductService
    {
        PaginationData<Product> GetAllProducts(QueryProduct query);

        Task<MessageResponse> SyncDataStore();
    }
}
