using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mango.Core.DataStructure;

namespace Mango.Core.Srd.Abstractions
{
    /// <summary>
    /// 服务发现接口
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// 请求服务
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        Task<ServiceEntity> DiscoveryService(string serviceName);
    }
}
