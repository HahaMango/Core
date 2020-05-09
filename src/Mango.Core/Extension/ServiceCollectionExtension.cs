using Mango.Core.AutoMapper;
using Microsoft.Extensions.DependencyInjection;


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
            return services;
        }
    }
}
