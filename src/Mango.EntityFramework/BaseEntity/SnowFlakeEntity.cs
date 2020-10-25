using Mango.KeyGenerator;
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
        private SnowFlakeGenerator _snowFlakeGenerator;

        /// <summary>
        /// 生成键
        /// </summary>
        public override void SetId()
        {
            if(_snowFlakeGenerator == null)
            {
                _snowFlakeGenerator = SnowFlakeGenerator.Instance();
            }
            Id = _snowFlakeGenerator.GetKey();
        }
    }
}
