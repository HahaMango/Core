using Mango.Core.KeyGenerator;
using Mango.Core.EntityFramework.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mango.Core.EntityFramework.BaseEntity
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public abstract class Entity : IBaseEntity<long>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 设置Id
        /// </summary>
        public abstract void SetId();
    }
}
