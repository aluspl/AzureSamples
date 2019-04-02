using LifeLike.Shared.Model;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLike.CloudService.TableStorage
{
    public class OrderEntity : TableEntity
    {
        public string Id { get => RowKey; set => RowKey = value; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Product> Products { get; set; }
        public OrderEntity()
        {
            PartitionKey = "order";
        }
        public static Order Convert(OrderEntity entity)
        {
            return new Order
            {
                ClientName = entity.ClientName,
                Date = entity.Date,
                Id = entity.Id,
                Products = entity.Products
            };
        }
        public static OrderEntity Convert(Order entity)
        {
            return new OrderEntity
            {
                ClientName = entity.ClientName,
                Date = entity.Date,
                Id = entity.Id,
                Products = entity.Products
            };
        }
    }    
}
