using Mango.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Mango.EntityFramework
{

    public class EfContextWork<TDbContext> : IEfContextWork
        where TDbContext : BaseDbContext
    {
        private readonly TDbContext _context;

        public EfContextWork(TDbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            return await _context.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            _context.Database.RollbackTransaction();
        }

        public int SaveChanges()
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));
            return await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    _context.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~EfContextWork()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
             GC.SuppressFinalize(this);
        }
        #endregion


    }
}
