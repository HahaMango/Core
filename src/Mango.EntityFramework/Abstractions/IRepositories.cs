using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.EntityFramework.Abstractions
{
    /// <summary>
    /// 仓储基础接口
    /// </summary>
    public interface IRepositories<TDbcontext,TEntity>
        where TEntity : class, IEntity , new()
        where TDbcontext : BaseDbContext
    {
        
    }
}
