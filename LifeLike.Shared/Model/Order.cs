using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLike.Shared.Model
{
    public class Order
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
