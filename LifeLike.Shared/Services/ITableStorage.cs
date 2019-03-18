using LifeLike.Shared.Enums;
using LifeLike.Shared.Model;
using LifeLike.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LifeLike.Shared.Services
{
    public interface ITableStorage
    {
        bool IsWorking { get;  }

        Task<IEnumerable<Item>> List();
        Task<Item> GetItem(string id);
        Task<Result> Create(Item item);

        Task<Result> Delete(Item item);
        Task<Result> DeleteAll(string v);
        Task<Result> Update(string id, Item value);
    }
}
