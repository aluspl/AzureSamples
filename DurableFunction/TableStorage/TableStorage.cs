using DurableFunction.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DurableFunction.Service
{ 
    public class TableStorage
    {
        private readonly string _table;
        private readonly CloudTableClient _tableClient;

        public bool IsWorking { get => _tableClient != null; }

        public TableStorage(string configuration, string table)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(configuration);
                _tableClient = storageAccount.CreateCloudTableClient();
                _table = table;
            }
            catch
            {
                _tableClient = null;
            }
        }

        private CloudTable GetTable(string tableName)
        {
            var _table = _tableClient.GetTableReference(tableName);
            _table.CreateIfNotExistsAsync().Wait();
            return _table;
        }


        public async Task<IEnumerable<ItemEntity>> List()
        {
            var query = new TableQuery<ItemEntity>();
            var result = await GetTable(_table).ExecuteQuerySegmentedAsync(query, null);
            return result.Results;            
        }
        public async Task<ItemEntity> GetItem(string partition, string row)
        {
            var query = TableOperation.Retrieve<ItemEntity>(partition, row);
            var result = await GetTable(_table).ExecuteAsync(query);
            return (ItemEntity)result.Result;
        }
        public async Task Create(ItemEntity item)
        {
           
            var insert = TableOperation.Insert(item);
            await GetTable(_table).ExecuteAsync(insert);
        }
        
        public async Task Delete(ItemEntity item)
        {

            var delete = TableOperation.Delete(item);
            await GetTable(_table).ExecuteAsync(delete);
        }
        public async  Task DeleteAll(string v)
        {
            await GetTable(v).DeleteAsync();
        }

        public async Task Update(string id, ItemEntity value)
        {

            var replace = TableOperation.Replace(value);
            await GetTable(_table).ExecuteAsync(replace);
        }
    }
}
