using LifeLike.Shared.Enums;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace LifeLike.CloudService.TableStorage
{
    internal class ItemEntity : TableEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }

        internal static ItemEntity Convert(Item item)
        {
            return new ItemEntity
            {

                RowKey = item.Id,
                PartitionKey = "ITEM",
                Value = item.Value
                   
            };
        }
        internal static Item Convert(ItemEntity item)
        {
            return new Item
            {  
                Id = item.RowKey,
                Name = item.Name,
                Value = item.Value
            };
        }
    }
}