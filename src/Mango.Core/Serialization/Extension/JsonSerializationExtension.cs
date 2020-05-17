using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Mango.Core.Serialization.Extension
{
    /// <summary>
    /// JSON序列化扩展类
    /// </summary>
    public static class JsonSerializationExtension
    {
        private static JsonSerializerOptions _options;

        static JsonSerializationExtension()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        /// <summary>
        /// 对象转json字符串 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static async Task<string> ToJsonAsync<T>(this T o)
            where T : class
        {
            return await o.ToJsonAsync<T>(_options);
        }

        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T o)
            where T : class
        {
            return o.ToJson<T>(_options);
        }

        /// <summary>
        /// 对象转utf8格式json字符传
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJsonUtf8<T>(this T o)
            where T :class
        {
            return o.ToJsonUtf8<T>(_options);
        }

        /// <summary>
        /// 通过配置，对象转json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T o, JsonSerializerOptions options)
            where T : class
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return JsonSerializer.Serialize<T>(o,options);
        }

        /// <summary>
        /// 通过配置，对象转utf8格式json字符传
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJsonUtf8<T>(this T o,JsonSerializerOptions options)
            where T : class
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<T>(o,options);
            return Encoding.UTF8.GetString(utf8Bytes);
        }

        /// <summary>
        /// 通过配置，对象转json字符串 异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static async Task<string> ToJsonAsync<T>(this T o,JsonSerializerOptions options)
            where T : class
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            string json = null;

            using (var steam = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync<T>(steam, o);
                using (var reader = new StreamReader(steam))
                {
                    json = await reader.ReadToEndAsync();
                }
            }

            return json;
        }
    }
}
