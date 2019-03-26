using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace DurableFunction.Models
{
    public class ItemEntity : TableEntity
    {
        public string Email { get; set; }

        public Item ToItem()
        {
            return new Item
            {
                Id = this.RowKey,
                Email = this.Email,
            };                 
        }
    }
}