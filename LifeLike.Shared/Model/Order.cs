using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLike.Shared.Model
{
    public class Order
    {
        public string RowKey { get; set; }
        public string Id { get => RowKey; set => RowKey=value; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
