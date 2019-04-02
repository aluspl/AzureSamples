using LifeLike.CloudService.Search;
using LifeLike.Shared.Model;
using System.Linq;

namespace LifeLike.CloudService.Mappings
{
    public static class SearchMappingExtension
    {
        public static Order Convert(this SearchOrder entity)
        {
            return new Order
            {
                ClientName = entity.ClientName,
                Date = entity.Date,
                Id = entity.Id,
                Products = entity.ProductList.Select(p => p.Convert()).ToList()
            };
        }
        public static SearchOrder Convert(this Order entity)
        {
            return new SearchOrder
            {
                ClientName = entity.ClientName,
                Date = entity.Date,
                Id = entity.Id,
                ProductList = entity.Products.Select(p => p.Convert()).ToArray()
            };
        }
    
        public static Product Convert(this SearchProduct entity)
        {
            return new Product
            {
                Name = entity.Name,
                Value = entity.Value,
                Id = entity.Id,
            };
        }
        public static SearchProduct Convert(this Product entity)
        {
            return new SearchProduct
            {
                Name = entity.Name,
                Value = entity.Value,
                Id = entity.Id,
            };
        }
    }
}
