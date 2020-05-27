using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Infrastructure.Helper
{
    /// <summary>
    /// 字符串帮助程序
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="array"></param>
        public static string ToHex(byte[] array)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append($"{array[i]:X2}");
                if ((i % 4) == 3) stringBuilder.Append(" ");
            }
            return stringBuilder.ToString();
        }
    }
}
