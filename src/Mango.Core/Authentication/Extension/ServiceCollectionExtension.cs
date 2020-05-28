using Mango.Core.Authentication.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Mango.Core.Authentication.Extension
{
    /// <summary>
    /// Mango Jwt处理程序的DI扩展类
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加Mango Jwt颁发处理程序到DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">jwt颁发处理器配置</param>
        /// <returns></returns>
        public static IServiceCollection AddMangoJwtHandler(this IServiceCollection services, Action<MangoJwtOptions> options)
        {
            var jwtOptions = new MangoJwtOptions();
            options(jwtOptions);
            var handler = new MangoJwtTokenHandler(jwtOptions);
            services.AddSingleton(handler);
            return services;
        }

        /// <summary>
        /// 添加jwt认证
        /// 
        /// 基于Audience字段权限控制，Audience字段形如x.y.z 。如果配置的Audience字段为x.y,则只有具有形如x.y.[z1.z2...zn]的Token才能够认证通过,如token只有x。则无法通过认证。
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">jwt认证配置</param>
        /// <returns></returns>
        public static IServiceCollection AddMangoJwtAuthentication(this IServiceCollection services,Action<MangoJwtValidationOptions> options)
        {
            var jwtOptions = new MangoJwtValidationOptions();
            options(jwtOptions);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(15),
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidIssuer = jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        AudienceValidator = (validAud, b, c) =>
                        {
                            foreach(var aud in validAud)
                            {
                                if(aud.Contains(c.ValidAudience))
                                {
                                    return true;
                                }
                            }
                            return false;
                        }
                    };
                });
            return services;
        }
    }
}
