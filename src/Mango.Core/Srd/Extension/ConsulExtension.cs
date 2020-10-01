using Consul;
using Mango.Core.DataStructure;
using Mango.Core.Srd.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Srd.Extension
{
    /// <summary>
    /// consul扩展方法
    /// </summary>
    public static class ConsulExtension
    {
        /// <summary>
        /// 向consul注册服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceRegistration"></param>
        /// <param name="serviceEntity"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IApplicationBuilder RegisterConsulService(this IApplicationBuilder app, IServiceRegistration serviceRegistration, MangoService serviceEntity, IHostApplicationLifetime lifetime)
        {
            //服务注册
            serviceRegistration.RegistrationService(serviceEntity);

            //结束时取消注册
            lifetime.ApplicationStopping.Register(() =>
            {
                serviceRegistration.DeregisterService(serviceEntity);
            });

            return app;
        }
    }
}
