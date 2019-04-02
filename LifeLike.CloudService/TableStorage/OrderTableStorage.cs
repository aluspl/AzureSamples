using LifeLike.Shared.Enums;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using LifeLike.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeLike.CloudService.TableStorage
{
    public class OrderTableStorage : IOrderTableStorage
    {
        private readonly CloudTableClient _tableClient;

        public bool IsWorking { get => _tableClient != null; }

        public OrderTableStorage(IConfiguration configuration)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(configuration["BlobStorage"]);
                _tableClient = storageAccount.CreateCloudTableClient();

            }
            catch
            {
                _tableClient = null;
            }
        }

        public CloudTable GetTable(string tableName)
        {
            var _table = _tableClient.GetTableReference(tableName);
            _table.CreateIfNotExistsAsync().Wait();
            return _table;
        }


        public async Task<IEnumerable<Order>> List()
        {
            var query = new TableQuery<OrderEntity>();
            var result = await GetTable("orders").ExecuteQuerySegmentedAsync(query, null);
            return result.Results.Select(item => OrderEntity.Convert(item));            
        }
        public async Task<Order> GetItem(string id)
        {
            var query = TableOperation.Retrieve<OrderEntity>("order",id);
            var result = await GetTable("orders").ExecuteAsync(query);
            return OrderEntity.Convert((OrderEntity)result.Result);
        }
        public async Task<Result> Create(Order item)
        {
            var model = OrderEntity.Convert(item);
           
            var insert = TableOperation.Insert(model);
            await GetTable("orders").ExecuteAsync(insert);
            return Result.Success;
        }
        
        public async Task<Result> Delete(Order item)
        {
            var model = OrderEntity.Convert(item);

            var delete = TableOperation.Delete(model);
            await GetTable("orders").ExecuteAsync(delete);
            return Result.Success;
        }
        public async  Task<Result> DeleteAll(string v)
        {
            await GetTable(v).DeleteAsync();
            return Result.Success;
        }

        public async Task<Result> Update(string id, Order value)
        {
            var model = OrderEntity.Convert(value);

            var replace = TableOperation.Replace(model);
            await GetTable("orders").ExecuteAsync(replace);
            return Result.Success;
        }
    }
}
