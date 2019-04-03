using Newtonsoft.Json;
using System;

namespace LifeLike.ElasticSearch.Models
{
    public class SearchOrder
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public SearchProduct[] ProductList{ get; set; }
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
