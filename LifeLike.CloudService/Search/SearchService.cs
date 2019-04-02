using LifeLike.CloudService.Mappings;
using LifeLike.Shared.Model;
using LifeLike.Shared.Services;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace LifeLike.CloudService.Search
{
    public class SearchService : ISearchService
    {
        private readonly string _IndexClient;
        private readonly SearchServiceClient _serviceClient;
        private readonly IOrderTableStorage _storage;

        public SearchService(IConfiguration configuration, IOrderTableStorage storage)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string ApiKey = configuration["SearchServiceAdminApiKey"];
            _IndexClient = configuration["IndexSearchClient"];
            _serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(ApiKey));
            _storage = storage;
            CreateIndex(_IndexClient, _serviceClient);
        }

        public void Create(Order order)
        {
            var batch = IndexBatch.Upload(new SearchOrder[] { order.Convert() });

            GetIndexClient(_IndexClient).Documents.Index(batch);
        }
        public void Update(string id, Order order)
        {
            var batch = IndexBatch.MergeOrUpload(new SearchOrder[] { order.Convert() });
            GetIndexClient(_IndexClient).Documents.Index(batch);
            _storage.Update(id, order).Wait();
        }
        public void Delete(string id)
        {
            var batch = IndexBatch.Delete(new SearchOrder[] { new SearchOrder { Id = id } });
            GetIndexClient(_IndexClient).Documents.Index(batch);

        }
        public ICollection<Order> GetMany(string filter)
        {
            var returns = new List<Order>();

            var results = GetIndexClient(_IndexClient).Documents.Search<SearchOrder>(filter);
            foreach (var result in results.Results)
            {
                returns.Add(result.Document.Convert());
            }
            return returns;
        }
        public Order Get(string filter)
        {
            var result = GetIndexClient(_IndexClient).Documents.Get<SearchOrder>(filter);
            return result.Convert();
        }


        private ISearchIndexClient GetIndexClient(string name)
        {
            return _serviceClient.Indexes.GetClient(name);
        }
        private static void CreateIndex(string indexName, SearchServiceClient serviceClient)
        {
            var definition = new Index()
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<SearchOrder>()
            };

            serviceClient.Indexes.CreateOrUpdate(definition);
        }
    }
}
