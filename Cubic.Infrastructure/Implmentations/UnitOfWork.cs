using Cubic.Core.Interfaces;
using Cubic.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Infrastructure.Implmentations
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Dictionary<Type, object> _repositories;
        private bool _disposed = false;

        private bool disposedValue;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            
            if (hasCustomRepository)
            {
                var customRepo = _context.GetService<IRepository<TEntity>>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                // create new repository instance
                var repository = new Repository<TEntity>(_context);

                _repositories[type] = repository;
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    
                    // dispose the db context.
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

     
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
