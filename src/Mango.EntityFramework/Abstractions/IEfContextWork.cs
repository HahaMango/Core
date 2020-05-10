using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.EntityFramework.Abstractions
{
    /// <summary>
    /// EF事务，上下文SaveChanges组件
    /// </summary>
    public interface IEfContextWork : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        IDbContextTransaction BeginTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync();

        void Rollback();

        void Commit();
    }
}
