using LifeLike.Shared.BlobStorage;
using LifeLike.Shared.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LifeLike.Shared.Services
{
    public interface IBlobStorage
    {
        Task<string> Create(BlobItem item);
        Task<Result> Update(BlobItem item);

        Task<Result> Remove(string name, string folder); 
        Task<BlobItem> Get(string name, string folder);
        Task<IEnumerable<BlobItem>> GetList(string folder);


    }
}
