using System;
using System.Collections.Generic;
using System.Linq;
using LifeLike.ElasticSearch.Mappings;
using LifeLike.ElasticSearch.Models;
using LifeLike.Shared.Model;
using LifeLike.Shared.Services;
using Microsoft.Extensions.Configuration;
using Nest;

namespace LifeLike.ElasticSearch
{
    internal class ElasticSearchService : ISearchService
    {
        private readonly ElasticClient _client;

        public ElasticSearchService(IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<SearchOrder>(m => m
                      .PropertyName(p => p.Id, "id")
                )
                .DefaultMappingFor<SearchProduct>(m => m                   
                    .PropertyName(c => c.Id, "id")
                );

            _client = new ElasticClient(settings);
        }
        public void Create(Order order)
        {
            _client.IndexDocument(order);
        }

        public void Delete(string id)
        {
            _client.DeleteByQuery<SearchOrder>(q=>q.QueryOnQueryString("id"));
        }

        public Order Get(string filter)
        {
            return _client.Get<SearchOrder>(new SearchOrder { }).Source.Convert();
        }

        public ICollection<Order> GetMany(string filter)
        {
            return _client.Search<SearchOrder>(s=>
            s.Query(q=>
            q.QueryString(d=>
            d.Query(filter))))
            .Documents.Select(p=>p.Convert()).ToList();
        }

        public void Update(string id, Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}