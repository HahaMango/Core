using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core.DataStructure
{
    public class PageList<T>
    {
        /// <summary>
        /// 分页内容
        /// </summary>
        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// 当前页项数量
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="data"></param>
        public PageList(int page, int size, int count, IEnumerable<T> data)
        {
            Page = page;
            Size = size;
            Count = count;
            Data = data;
        }
    }
}
