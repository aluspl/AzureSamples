using System.Collections.Generic;

namespace LifeLike.Shared.Services
{
    public interface ISearchService
    {
        ICollection<T> GetMany<T>(string filter) where T : class;
        T Get<T>(string filter) where T : class;
    }
}