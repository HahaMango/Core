using Mango.Core.HttpService;
using Mango.Core.Serialization.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Infrastructure.Helper
{
    /// <summary>
    /// http请求静态类
    /// </summary>
    public static class HttpHelper
    {
        private readonly static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<HttpResponse<string>> GetAsync(string url,Dictionary<string,string> param,string token = null)
        {
            var httpResponse = new HttpResponse<string>();
            #region 构造URL
            var queryString =  BuildQueryString(param);
            //queryString = URLEncode(queryString);
            url += queryString;
            #endregion
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (token != null)
            {
                requestMessage.Headers.Add(HttpRequestHeaderConst.Authorization.HeaderKeyName, HttpRequestHeaderConst.Authorization.Bearer + token);
            }
            var response = await _httpClient.SendAsync(requestMessage);
            httpResponse.StatusCode = response.StatusCode;
            httpResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;
            var content = default(string);
            using (MemoryStream ms = new MemoryStream())
            {
                await response.Content.CopyToAsync(ms);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    content = await sr.ReadToEndAsync();
                    httpResponse.Data = content;
                }
            }
            return httpResponse;
        }

        /// <summary>
        /// Post请求（媒体类型默认为json）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="mediaType">默认为json</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<HttpResponse<string>> PostAsync(string url,string body,string mediaType = "application/json", string token = null)
        {
            var httpResponse = new HttpResponse<string>();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(body, Encoding.UTF8, mediaType);
            if (token != null)
            {
                requestMessage.Headers.Add(HttpRequestHeaderConst.Authorization.HeaderKeyName, HttpRequestHeaderConst.Authorization.Bearer + token);
            }
            var response = await _httpClient.SendAsync(requestMessage);
            httpResponse.StatusCode = response.StatusCode;
            httpResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;
            var responseResult = default(string);
            using (MemoryStream ms = new MemoryStream())
            {
                await response.Content.CopyToAsync(ms);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    responseResult = await sr.ReadToEndAsync();
                    httpResponse.Data = responseResult;
                }
            }
            return httpResponse;
        }

        /// <summary>
        /// Get请求（json序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<HttpResponse<T>> GetAsync<T>(string url, Dictionary<string, string> param, string token = null)
            where T : class, new()
        {
            var httpResponse = new HttpResponse<T>();
            #region 构造URL
            var queryString = BuildQueryString(param);
            //queryString = URLEncode(queryString);
            url += queryString;
            #endregion
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (token != null)
            {
                requestMessage.Headers.Add(HttpRequestHeaderConst.Authorization.HeaderKeyName, HttpRequestHeaderConst.Authorization.Bearer + token);
            }
            var response = await _httpClient.SendAsync(requestMessage);
            httpResponse.StatusCode = response.StatusCode;
            httpResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;
            var content = default(string);
            using (MemoryStream ms = new MemoryStream())
            {
                await response.Content.CopyToAsync(ms);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    content = await sr.ReadToEndAsync();
                    httpResponse.Data = await content.ToObjectAsync<T>();
                }
            }
            return httpResponse;
        }

        /// <summary>
        /// Post请求（json序列化）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonBody"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<HttpResponse<T>> PostAsync<T>(string url, string jsonBody, string token = null)
            where T : class, new()
        {
            var httpResponse = new HttpResponse<T>();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, HttpRequestHeaderConst.ContentType.JSON);
            if (token != null)
            {
                requestMessage.Headers.Add(HttpRequestHeaderConst.Authorization.HeaderKeyName, HttpRequestHeaderConst.Authorization.Bearer + token);
            }
            var response = await _httpClient.SendAsync(requestMessage);
            httpResponse.StatusCode = response.StatusCode;
            httpResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;
            var responseResult = default(string);
            using (MemoryStream ms = new MemoryStream())
            {
                await response.Content.CopyToAsync(ms);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    responseResult = await sr.ReadToEndAsync();
                    httpResponse.Data = await responseResult.ToObjectAsync<T>();
                }
            }
            return httpResponse;
        }


        #region 辅助函数
        /// <summary>
        /// 构造查询字符串
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string BuildQueryString(Dictionary<string,string> param)
        {
            var stringBuilder = new StringBuilder("?");
            foreach(var keypair in param)
            {
                stringBuilder.Append($"{keypair.Key}={keypair.Value}&");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string URLEncode(string input)
        {
            return System.Web.HttpUtility.UrlEncode(input);
        }

        #endregion
    }
}
