using LifeLike.Shared.Services;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace LifeLike.CloudService.Search
{
    public class SearchService : ISearchService
    {
        private readonly SearchServiceClient _serviceClient;

        private readonly SearchIndexClient _indexClient;

        public SearchService(IConfiguration configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string ApiKey = configuration["SearchServiceAdminApiKey"];

            _serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(ApiKey));
            _indexClient = new SearchIndexClient(searchServiceName, "orders", new SearchCredentials(ApiKey));
        }
        private ISearchIndexClient GetIndexClient(string name)
        {
            return _serviceClient.Indexes.GetClient(name);

        }
        public ICollection<T> GetMany<T>(string filter) where T : class
        {
            ICollection < T > returns= new List<T>();
          
            var results = GetIndexClient("orders").Documents.Search<T>(filter);
            foreach(var result in results.Results)
            {
                returns.Add(result.Document);
            }
            return returns;
        }
        public T Get<T>(string filter) where T : class
        {
          
            var results = GetIndexClient("orders").Documents.Get<T>(filter);
            return results;
        }
    }
}
