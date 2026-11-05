using System;
using System.Threading.Tasks;

namespace SmartFix.Infrastructur.Interfaces
{
    /// <summary>
    /// UnitOfWork Pattern: يضمن ت atomicity على عمليات متعددة على قاعدة البيانات.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}