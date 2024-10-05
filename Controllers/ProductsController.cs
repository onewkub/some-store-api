using Microsoft.AspNetCore.Mvc;
using SomeStoreAPI.DTOs;
using SomeStoreAPI.Models;
using SomeStoreAPI.Services;

namespace SomeStoreAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductsController : ControllerBase
    {

        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet()]
        public PaginationData<Product> GetAllProduct([FromQuery] QueryProduct query)
        {

            var result = _productService.GetAllProducts(query);

            return result;
        }

        [HttpGet("sync")]
        public Task<MessageResponse> SyncProductData()
        {
            return _productService.SyncDataStore();
        }
    }
}
