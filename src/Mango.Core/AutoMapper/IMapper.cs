using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.AutoMapper
{
    public interface IMapper
    {
        /// <summary>
        /// 源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination);

        /// <summary>
        /// 源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDestination MapTo<TDestination>(object source) where TDestination : new();
    }
}
