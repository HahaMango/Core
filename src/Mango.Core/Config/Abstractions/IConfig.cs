using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Config.Abstractions
{
    /// <summary>
    /// 配置接口
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 根据键值获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T">自定义配置对象</typeparam>
        /// <returns></returns>
        Task<T> GetConfig<T>(string key) 
            where T :class,new();

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="config"></param>
        /// <typeparam name="T">自定义配置对象</typeparam>
        /// <returns></returns>
        Task<bool> SetConfig<T>(string key, T config) 
            where T:class,new();
    }
}
