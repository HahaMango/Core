﻿using Mango.EntityFramework.Abstractions;
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
        public static IServiceCollection AddMangoDbContext<TDbContext,TEFContextWork>(this IServiceCollection services,string connnectionString) 
            where TDbContext : BaseDbContext
            where TEFContextWork : class, IEfContextWork
        {
            services.AddDbContext<TDbContext>(config =>
            {
                config.UseMySql(connnectionString);
            });
            services.AddScoped<IEfContextWork, TEFContextWork>();
            return services;
        }
    }
}
