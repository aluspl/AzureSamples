using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LifeLike.CloudService.Search
{
    [SerializePropertyNamesAsCamelCase]
    public class SearchOrder
    {
        [Key]
        public string RowKey { get; set; }
        public string Id { get => RowKey; set => RowKey = value; }
        [IsFilterable, IsSortable, IsSearchable]
        public string ClientName { get; set; }
        [IsFilterable, IsSortable]
        public DateTime Date { get; set; }
        [JsonIgnore]
        public SearchProduct[] ProductList{ get; set; }
        [IsFilterable, IsSortable, IsSearchable]
        public string Products
        {
            get => ProductList != null ? JsonConvert.SerializeObject(ProductList) : null;
            set
            {
                if (value != null)
                    ProductList = JsonConvert.DeserializeObject<SearchProduct[]>(value);              
            }
        }
    }
}
