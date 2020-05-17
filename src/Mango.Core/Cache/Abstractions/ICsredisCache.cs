using Mango.Core.Cache.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Cache.Abstractions
{
    /// <summary>
    /// csredis缓存接口
    /// </summary>
    public interface ICsredisCache : ICache
    {
        /// <summary>
        /// 配置
        /// </summary>
        MangoRedisOptions Options { get; }
    }
}
