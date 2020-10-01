using Consul;
using Mango.Core.DataStructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Config
{
    /// <summary>
    /// mango服务配置中心包装类
    /// </summary>
    public class MangoConfig : ConsulConfig
    {
        /// <summary>
        /// 使用配置中心主机初始化
        /// </summary>
        /// <param name="host"></param>
        /// <param name="token"></param>
        public MangoConfig(string host,string token = null) : base(host, token)
        {

        }

        /// <summary>
        /// 使用consulClient初始化
        /// </summary>
        /// <param name="consulClient"></param>
        public MangoConfig(IConsulClient consulClient) : base(consulClient)
        {

        }

        /// <summary>
        /// 根据键值获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<MangoServiceConfiguration> GetConfig(string key)
        {
            return await base.GetConfig<MangoServiceConfiguration>(key);
        }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<bool> SetConfig(string key, MangoServiceConfiguration config)
        {
            return await base.SetConfig<MangoServiceConfiguration>(key, config);
        }
    }
}
