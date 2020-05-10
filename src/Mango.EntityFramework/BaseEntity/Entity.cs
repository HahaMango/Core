using Mango.EntityFramework.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.EntityFramework.BaseEntity
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public class Entity : IBaseEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 设置Id
        /// </summary>
        public void SetId()
        {
            throw new NotImplementedException();
        }
    }
}
