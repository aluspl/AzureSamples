using LifeLike.Shared.Enums;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LifeLike.Shared.Services
{
    public interface IOrderTableStorage
    {
        bool IsWorking { get;  }

        Task<IEnumerable<Order>> List();
        Task<Order> GetItem(string id);
        Task<Result> Create(Order item);

        Task<Result> Delete(Order item);
        Task<Result> DeleteAll(string v);
        Task<Result> Update(string id, Order value);
    }
}
