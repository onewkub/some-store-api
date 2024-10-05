using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SomeStoreAPI.Data;
using SomeStoreAPI.DTOs;
using SomeStoreAPI.Models;
using SomeStoreAPI.Repositories;
using System.Linq;

namespace SomeStoreAPI.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _productRepository;
        private readonly ApplicationDbContext _context;

        public ProductService(
            IRepository<Product> productRepository, 
            ApplicationDbContext context
            )
        {

            _productRepository = productRepository;
            _context = context;
        }

        public PaginationData<Product> GetAllProducts(QueryProduct query)
        {
            var queryAble = _productRepository
                .Find(p => ( query.Search == null || query.Search == string.Empty ) || p.Name.Contains(query.Search))
                .Include(p => p.Tags)
                .Include(p => p.Categories)
                .Include(p => p.Images);

            // Apply dynamic sorting based on OrderBy and OrderType
            var result = ApplyOrdering(queryAble, query.OrderBy, query.OrderType);

            // Pagination: Apply Skip and Take
            if(query.Skip != null && query.Take != null)
            {
                result = result.Skip((int)query.Skip).Take((int)query.Take);
            }

            var data = result.ToList();
            var count = queryAble.Count();

            return new PaginationData<Product> { Data = data, Count = count };
        }

        private IQueryable<Product> ApplyOrdering(IQueryable<Product> query, string orderBy, string orderType)
        {
            if (string.IsNullOrEmpty(orderBy))
                return query;

            // Dynamically order based on the property name and order type
            switch (orderBy.ToLower())
            {
                case "name":
                    query = orderType.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name);
                    break;
    
                case "created":
                    query = orderType.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Created)
                        : query.OrderBy(p => p.Created);
                    break;

                // Add more cases as needed for other fields you want to sort by
                default:
                    // Default to ordering by Name if no valid OrderBy field is provided
                    query = orderType.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name);
                    break;
            }

            return query;
        }


        public async Task<MessageResponse> SyncDataStore()
        {
            var httpRequest = new HttpClient();

            var response = await httpRequest.PostAsJsonAsync("https://tabledusud.nl/_product/simpleFilters", new
            {
                Page = 1,
                PageSize = 50,
            });

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var syncData = JsonConvert.DeserializeObject<SyncData>(responseContent);

            var products = syncData.Products.ToList();

            var tags = products.SelectMany(p => p.Tags).DistinctBy(t => t.Id).ToList();

            var categories = products.SelectMany(p => p.Categories).DistinctBy(c => c.Id).ToList();

            var importedProduct = products.Select(i => new Product
            {
                ProductId = i.ProductId,
                Name = i.Name,
                Title = i.Title,
                ThumbnailImage = i.ThumbnailImage,
                Price = i.Price,
                OriginalPrice = i.OriginalPrice,
                Brand = i.Brand,
                Sold = i.Sold,
                AllowMultipleConfigs = i.AllowMultipleConfigs,
                Url = i.Url,
                Created = i.Created,
                ReviewScore = i.ReviewScore,
                ReviewCount = i.ReviewCount,
                FullPriceBeforeOverallDiscount = i.FullPriceBeforeOverallDiscount,
                PossibleDiscountPrice = i.PossibleDiscountPrice,
                Images = i.Images,
                Categories = i.Categories,
                Tags = i.Tags,
            }).ToList();

            //await _tagRepository.UpsertRangeAsync(tags);

            //await _categoryRepository.UpsertRangeAsync(categories);

            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var product in importedProduct)
                    {
                        var existed = _context.Products
                            .Where(p => p.ProductId == product.ProductId)
                            .Include(p => p.Categories)
                            .Include(p => p.Tags)
                            .FirstOrDefault();

                        if (existed != null)
                        {
                            // Clear existing relationships
                            existed.Tags.Clear();
                            existed.Categories.Clear();

                            // Reassign Tags
                            foreach (var tag in product.Tags)
                            {
                                // Find existing tag in the context or attach it
                                var existingTag = _context.Tags.Local.FirstOrDefault(t => t.Id == tag.Id)
                                                  ?? _context.Tags.Find(tag.Id);

                                if (existingTag != null)
                                {
                                    existed.Tags.Add(existingTag);  // Reuse the existing tracked tag
                                }
                                else
                                {
                                    existed.Tags.Add(tag);  // If not found, add the tag
                                }
                            }

                            // Reassign Categories
                            foreach (var category in product.Categories)
                            {
                                // Find existing category in the context or attach it
                                var existingCategory = _context.Categories.Local.FirstOrDefault(c => c.Id == category.Id)
                                                       ?? _context.Categories.Find(category.Id);

                                if (existingCategory != null)
                                {
                                    existed.Categories.Add(existingCategory);  // Reuse the existing tracked category
                                }
                                else
                                {
                                    existed.Categories.Add(category);  // If not found, add the category
                                }
                            }

                            _context.Update(existed);  // Update the existing product
                        }
                        else
                        {

                            var productTags = product.Tags.ToList();
                            var productCategories = product.Categories.ToList();

                            // If the product does not exist, add it as a new product

                            product.Tags.Clear();
                            product.Categories.Clear();



                            // Reassign Tags
                            foreach (var tag in productTags)
                            {
                                // Find existing tag in the context or attach it
                                var existingTag = _context.Tags.Local.FirstOrDefault(t => t.Id == tag.Id)
                                                  ?? _context.Tags.Find(tag.Id);

                                if (existingTag != null)
                                {
                                    product.Tags.Add(existingTag);  // Reuse the existing tracked tag
                                }
                                else
                                {
                                    product.Tags.Add(tag);  // If not found, add the tag
                                }
                            }

                            // Reassign Categories
                            foreach (var category in productCategories)
                            {
                                // Find existing category in the context or attach it
                                var existingCategory = _context.Categories.Local.FirstOrDefault(c => c.Id == category.Id)
                                                       ?? _context.Categories.Find(category.Id);

                                if (existingCategory != null)
                                {
                                    product.Categories.Add(existingCategory);  // Reuse the existing tracked category
                                }
                                else
                                {
                                    product.Categories.Add(category);  // If not found, add the category
                                }
                            }

                            _context.Products.Add(product);

                        }
                        await _context.SaveChangesAsync();

                        _context.ChangeTracker.Clear();
                    }

                    transaction.Commit();

                }

                catch (Exception ex) 
                {
                    transaction.Rollback();
                    throw;
                }
            }



            return new MessageResponse
            {
                Message = $"Import and Update {syncData.Products.Count()} items"
            };

        }

    }
}
