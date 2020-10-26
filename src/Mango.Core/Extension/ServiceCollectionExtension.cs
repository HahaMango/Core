using Mango.Core.AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mango.Core.Extension
{
    /// <summary>
    /// 服务容器拓展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加AutoMapper支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapper = new MangoMapper();
            services.AddSingleton<IMapper>(mapper);
            MangoMapperExtension.SetMapper(mapper);
            return services;
        }

        /// <summary>
        /// 添加MediatR
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            var ass = AppDomain.CurrentDomain.GetAssemblies();
            services.AddMediatR(ass);
            return services;
        }
    }
}
