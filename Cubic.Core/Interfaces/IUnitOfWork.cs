using Cubic.Core.Entities;

namespace Cubic.Core.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        Task<int> SaveChangesAsync();

        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;

    }
}
