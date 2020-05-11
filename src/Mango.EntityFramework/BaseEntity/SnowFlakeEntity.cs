using Mango.Core.KeyGenerator;
using Mango.Core.KeyGenerator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.EntityFramework.BaseEntity
{
    /// <summary>
    /// 雪花算法实体
    /// </summary>
    public class SnowFlakeEntity : Entity
    {
        /// <summary>
        /// Id生成器
        /// </summary>
        private readonly IGenerator<long> _generator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="generator"></param>
        public SnowFlakeEntity(IGenerator<long> generator)
        {
            _generator = generator;
        }

        /// <summary>
        /// 生成键
        /// </summary>
        public override void SetId()
        {
            Id = _generator.GetKey();
        }
    }
}
