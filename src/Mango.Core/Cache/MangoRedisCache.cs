using CSRedis;
using Mango.Core.Cache.Abstractions;
using Mango.Core.Cache.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mango.Core.Cache
{
    /// <summary>
    /// 管理redis链接，数据访问。只是单纯封装一下csredis。
    /// </summary>
    public class MangoRedisCache : ICsredisCache
    {
        /// <summary>
        /// 配置
        /// </summary>
        public MangoRedisOptions Options { get; }

        public MangoRedisCache(MangoRedisOptions options)
        {
            if(string.IsNullOrEmpty(options.ConnectionString))
            {
                throw new ArgumentNullException(nameof(options.ConnectionString));
            }
            if (options.Sentinels == null || options.Sentinels.Count() <= 0)
            {
                var csredisClient = new CSRedisClient(options.ConnectionString);
                RedisHelper.Initialization(csredisClient);
            }
            else
            {
                var csredisSentinels = new CSRedisClient(options.ConnectionString, options.Sentinels);
                RedisHelper.Initialization(csredisSentinels);
            }
            Options = options;
        }
    }
}
