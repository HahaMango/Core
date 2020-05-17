using Mango.Core.Cache.Abstractions;
using Mango.Core.Cache.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Cache.Extension
{
    /// <summary>
    /// 添加缓存依赖注入
    /// </summary>
    public static class MangoCacheExtension
    {
        public static IServiceCollection AddMangoRedis(this IServiceCollection services,Action<MangoRedisOptions> options)
        {
            var op = new MangoRedisOptions();
            options(op);
            MangoRedisCache mangoRedisCache = new MangoRedisCache(op);
            services.AddSingleton<ICache>(mangoRedisCache);
            return services;
        }
    }
}
