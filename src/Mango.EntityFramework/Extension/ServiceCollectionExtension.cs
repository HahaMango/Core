using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.EntityFramework.Extension
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加Mango DB上下文
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMangoDbContext<TDbContext>(this IServiceCollection services,string connnectionString) where TDbContext : BaseDbContext
        {
            services.AddDbContext<TDbContext>(config =>
            {
                config.UseMySql(connnectionString);
            });
            return services;
        }
    }
}
