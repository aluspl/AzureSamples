using LifeLike.Shared.Model;
using System.Collections.Generic;

namespace LifeLike.Shared.Services
{
    public interface ISearchService
    {
        ICollection<Order> GetMany(string filter);
        Order Get(string filter);
         void Create(Order order);
         void Update(string id, Order order);
         void Delete(string id);
    }
}