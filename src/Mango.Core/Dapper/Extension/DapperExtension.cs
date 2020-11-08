using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Dapper.Extension
{
    /// <summary>
    /// Dapper扩展类
    /// </summary>
    public static class DapperExtension
    {
        /// <summary>
        /// 添加Dapper支持
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, string connectionString)
        {
            var dh = new DapperHelper(connectionString);
            services.AddSingleton(dh);
            return services;
        }
    }
}
