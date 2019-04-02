using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace LifeLike.CloudService.Search
{
    [SerializePropertyNamesAsCamelCase]
    public class SearchProduct
    {
        public string Id { get; set; }
        [IsFilterable, IsSortable, IsSearchable]
        public string Name { get; set; }
        [IsFilterable, IsSortable, IsSearchable]
        public int Value { get; set; }
    }
}