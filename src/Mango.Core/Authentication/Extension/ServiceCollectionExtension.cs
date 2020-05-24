using Mango.Core.Authentication.Jwt;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Mango.Core.Authentication.Extension
{
    /// <summary>
    /// Mango Jwt处理程序的DI扩展类
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加Mango Jwt处理程序到DI
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMangoJWTHandler(this IServiceCollection services, Action<MangoJwtOptions> options)
        {
            var jwtOptions = new MangoJwtOptions();
            options(jwtOptions);
            var handler = new MangoJwtTokenHandler(jwtOptions);
            services.AddSingleton(handler);
            return services;
        }
    }
}
