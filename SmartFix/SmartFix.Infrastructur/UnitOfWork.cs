using System;
using System.Threading.Tasks;
using SmartFix.Infrastructur.Interfaces;
using SmartFix.Infrastructur.Data;

namespace SmartFix.Infrastructur
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartFixDbContext _context;
        private bool _disposed;

        public UnitOfWork(SmartFixDbContext context)
        {
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}