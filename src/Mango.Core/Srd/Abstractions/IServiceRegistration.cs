using System;
using System.Collections.Generic;
using System.Text;
using Mango.Core.DataStructure;
using System.Threading.Tasks;

namespace Mango.Core.Srd.Abstractions
{
    /// <summary>
    /// 服务注册接口
    /// </summary>
    public interface IServiceRegistration
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        Task<bool> RegistrationService(ServiceEntity service);

        /// <summary>
        /// 取消服务注册
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        Task<bool> DeregisterService(ServiceEntity service);

        /// <summary>
        /// 取消服务注册（通过服务id）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeregisterServiceById(string Id);
    }
}
