using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mango.Core.Enums
{
    /// <summary>
    /// 返回状态枚举
    /// </summary>
    public enum Code
    {
        [Description("成功")]
        OK = 200,

        [Description("找不到")]
        NotFound = 404
    }
}
