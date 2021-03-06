﻿using Mango.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.ApiResponse
{
    /// <summary>
    /// 返回json对象
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public Code Code { get; set; }

        /// <summary>
        /// 返回字符串说明
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 返回json对象，包含数据内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 返回的json数据内容
        /// </summary>
        public T Data { get; set; }
    }
}
