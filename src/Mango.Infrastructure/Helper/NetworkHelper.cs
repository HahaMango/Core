using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Mango.Infrastructure.Helper
{
    /// <summary>
    /// 网络帮助类
    /// </summary>
    public class NetworkHelper
    {
        /// <summary>
        /// 获取主机名
        /// </summary>
        /// <returns></returns>
        public static string HostName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        /// 获取主机第一个IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress FirstLocalAddress()
        {
            return Dns.GetHostAddresses(HostName()).FirstOrDefault();
        }

        /// <summary>
        /// 获取主机IP地址列表
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] LocalAddressList()
        {
            return Dns.GetHostAddresses(HostName());
        }

        /// <summary>
        /// 获取主机内网IP地址列表
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] InternalLocalAddressList()
        {
            var ipList = LocalAddressList();
            var result = new List<IPAddress>();
            foreach (var ip in ipList)
            {
                var bits = ip.ToString().Split('.');
                if (bits.Length < 2)
                    continue;
                var flag = int.TryParse(bits[0], out int num);
                if (!flag)
                    continue;
                if(num == 10)
                {
                    result.Add(ip);
                }
                else if(num == 192)
                {
                    flag = int.TryParse(bits[1], out num);
                    if (flag)
                    {
                        if (num == 168)
                        {
                            result.Add(ip);
                        }
                    }
                }
                else if(num == 172)
                {
                    flag = int.TryParse(bits[1], out num);
                    if (flag)
                    {
                        if(num >= 16 && num <= 31)
                        {
                            result.Add(ip);
                        }
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 获取主机第一个内网IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress FirstInternalLocalAddress()
        {
            return InternalLocalAddressList().FirstOrDefault();
        }
    }
}
