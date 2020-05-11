using Mango.Core.KeyGenerator.Abstractions;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.KeyGenerator
{
    /// <summary>
    /// 雪花Id生成器
    /// </summary>
    public class SnowFlakeGenerator : IGenerator<long>
    {
        /// <summary>
        /// 雪花生成器（必须为单例，否则可能导致重复键值）
        /// </summary>
        private readonly IdWorker _idWorker;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SnowFlakeGenerator()
        {
            _idWorker = new IdWorker(1, 1);
        }

        /// <summary>
        /// 生成键
        /// </summary>
        /// <returns></returns>
        public long GetKey()
        {
            return _idWorker.NextId();
        }
    }
}
