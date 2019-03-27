using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace QueueDBFunction.Models
{
  
    public class DBItemEntity : TableEntity
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DBItemEntity()
        {
            Id = Guid.NewGuid().ToString();
            RowKey = Id;
        }
    }
}
