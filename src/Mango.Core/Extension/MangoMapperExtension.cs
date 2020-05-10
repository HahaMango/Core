﻿using Mango.Core.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.Extension
{
    /// <summary>
    /// 映射扩展类
    /// </summary>
    public static class MangoMapperExtension
    {
        private static IMapper _mapper;

        public static void SetMapper(IMapper mapper)
        {
            if(mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
            _mapper = mapper;
        }

        /// <summary>
        /// 执行映射
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
        {
            return _mapper.MapTo<TDestination>(source);
        }
    }
}
