using LifeLike.Shared.Enums;

namespace LifeLike.Shared
{
    public interface IUnitOfWork
    {
        Provider Provider { get;  }
        IRepository<T> Get<T>()  where T : class;
        Result Save();
    }
}