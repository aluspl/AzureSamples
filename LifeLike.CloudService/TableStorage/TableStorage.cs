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
    public class TableStorage : ITableStorage
    {
        private readonly CloudTableClient _tableClient;

        public bool IsWorking { get => _tableClient != null; }

        public TableStorage(IConfiguration configuration)
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

        private CloudTable GetTable(string tableName)
        {
            var _table = _tableClient.GetTableReference("photos");
            _table.CreateIfNotExistsAsync().Wait();
            return _table;
        }


        public async Task<IEnumerable<Item>> List()
        {
            var query = new TableQuery<ItemEntity>();
            var result = await GetTable("Item").ExecuteQuerySegmentedAsync(query, null);
            return result.Results.Select(item => ItemEntity.Convert(item));            
        }
        public async Task<Item> GetItem(string id)
        {
            var query = TableOperation.Retrieve<ItemEntity>("ITEM",id);
            var result = await GetTable("Item").ExecuteAsync(query);
            return ItemEntity.Convert((ItemEntity)result.Result);
        }
        public async Task<Result> Create(Item item)
        {
            var model = ItemEntity.Convert(item);
           
            var insert = TableOperation.Insert(model);
            await GetTable("Item").ExecuteAsync(insert);
            return Result.Success;
        }
        
        public async Task<Result> Delete(Item item)
        {
            var model = ItemEntity.Convert(item);

            var delete = TableOperation.Delete(model);
            await GetTable("Item").ExecuteAsync(delete);
            return Result.Success;
        }
        public async  Task<Result> DeleteAll(string v)
        {
            await GetTable(v).DeleteAsync();
            return Result.Success;
        }

        public async Task<Result> Update(string id, Item value)
        {
            var model = ItemEntity.Convert(value);

            var replace = TableOperation.Replace(model);
            await GetTable("Item").ExecuteAsync(replace);
            return Result.Success;
        }
    }
}
