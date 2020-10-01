using Consul;
using Mango.Core.Config.Abstractions;
using Mango.Core.Serialization.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Config
{
    /// <summary>
    /// consul配置中心
    /// </summary>
    public class ConsulConfig : IConfig
    {
        private readonly IConsulClient _consulClient;
        private readonly string _token;

        /// <summary>
        /// 使用配置中心主机初始化
        /// </summary>
        /// <param name="host"></param>
        /// <param name="token"></param>
        public ConsulConfig(string host,string token = null)
        {
            _consulClient = new ConsulClient(x => x.Address = new Uri(host));
            _token = token;
        }

        /// <summary>
        /// 使用consulClient初始化
        /// </summary>
        /// <param name="consulClient"></param>
        public ConsulConfig(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        /// <summary>
        /// 根据键值获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T">自定义配置对象</typeparam>
        /// <returns></returns>
        public async Task<T> GetConfig<T>(string key) 
            where T : class, new()
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var options = new QueryOptions
            {
                Token = _token
            };

            var consulResponse = await _consulClient.KV.Get(key, options);
            if (consulResponse.Response == null)
                return null;
            var bytes = consulResponse.Response.Value;
            var jsonString = BytesToString(bytes);
            return await jsonString.ToObjectAsync<T>();
        }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="config"></param>
        /// <typeparam name="T">自定义配置对象</typeparam>
        /// <returns></returns>
        public async Task<bool> SetConfig<T>(string key, T config) 
            where T : class,new()
        {
            if(config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            if(key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var jsonString = config.ToJson();
            var bytes = StringToBytes(jsonString);
            var kv = new KVPair(key)
            {
                Value = bytes
            };

            var options = new WriteOptions
            {
                Token = _token
            };

            var result = await _consulClient.KV.Put(kv, options);
            if (!result.Response)
            {
                return false;
            }
            return true;
        }

        private string BytesToString(byte[] value)
        {
            return Encoding.UTF8.GetString(value);
        }

        private byte[] StringToBytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }
}
