using Consul;
using Mango.Core.DataStructure;
using Mango.Core.Srd.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Srd
{
    /// <summary>
    /// consul服务注册实现
    /// </summary>
    public class ConsulRegistration : IServiceRegistration
    {
        private readonly IConsulClient _consulClient;

        /// <summary>
        /// 服务注册延时
        /// </summary>
        public TimeSpan? DeregisterCriticalServiceAfter { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// 健康检查事件间隔
        /// </summary>
        public TimeSpan? Interval { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// 超时时间
        /// </summary>
        public TimeSpan? Timeout { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// 健康检查url
        /// </summary>
        public string HealthCheckUrl { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="host"></param>
        /// <param name="token"></param>
        public ConsulRegistration(string host, string token = null)
        {
            _consulClient = new ConsulClient(x =>
            {
                x.Address = new Uri(host);
                x.Token = token;
            });
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public Task<bool> DeregisterService(ServiceEntity service)
        {
            _consulClient.Agent.ServiceDeregister(service.Id).Wait();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<bool> DeregisterServiceById(string Id)
        {
            _consulClient.Agent.ServiceDeregister(Id).Wait();
            return Task.FromResult(true);
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public Task<bool> RegistrationService(ServiceEntity service)
        {
            if(service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (string.IsNullOrEmpty(HealthCheckUrl))
            {
                throw new NullReferenceException("请设置服务健康检查Url");
            }

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = DeregisterCriticalServiceAfter,
                Interval = Interval,
                HTTP = HealthCheckUrl,
                Timeout = Timeout
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = service.Id,
                Name = service.ServiceName,
                Address = service.IP,
                Port = Convert.ToInt32(service.Port),
                //Tags = new[] { $"urlprefix-/{service.ServiceName}" }
            };

            _consulClient.Agent.ServiceRegister(registration).Wait();

            return Task.FromResult(true);
        }
    }
}
