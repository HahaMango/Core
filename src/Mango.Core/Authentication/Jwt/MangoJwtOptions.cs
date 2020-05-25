using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Authentication.Jwt
{
    /// <summary>
    /// Jwt配置类
    /// </summary>
    public class MangoJwtOptions
    {
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 过期时间（单位：秒）
        /// </summary>
        public int? ExpiresSec { get; set; }

        /// <summary>
        /// 域
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 颁发机构
        /// </summary>
        public string Issuer { get; set; }
    }
}
