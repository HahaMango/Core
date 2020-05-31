using Mango.Core.HttpService;
using Mango.Core.Serialization.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Infrastructure.HttpService
{
    /// <summary>
    /// json http服务实现
    /// </summary>
    public class JsonHttpService<T> : IJsonHttpService<T>
        where T : class, new()
    {
        private readonly HttpClient _httpClient;

        public JsonHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add(HttpRequestHeaderConst.Accept.HeaderKeyName, HttpRequestHeaderConst.Accept.JSON);
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token">jwt身份令牌</param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> GetAsync(string url, string token = null)
        {
            var httpResponse = new HttpResponse<T>();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if(token != null)
            {
                requestMessage.Headers.Add(HttpRequestHeaderConst.Authorization.HeaderKeyName, HttpRequestHeaderConst.Authorization.Bearer + token);
            }
            var response = await _httpClient.SendAsync(requestMessage);
            httpResponse.StatusCode = response.StatusCode;
            httpResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;
            var content = default(string);
            using(MemoryStream ms = new MemoryStream())
            {
                await response.Content.CopyToAsync(ms);
                ms.Position = 0;
                using (StreamReader sr =  new StreamReader(ms,Encoding.UTF8))
                {
                    content = await sr.ReadToEndAsync();
                }
            }
            if (!string.IsNullOrEmpty(content))
            {
                var contentJson = await content.ToObjectAsync<T>();
                httpResponse.Data = contentJson;
                return httpResponse;
            }
            return await Task.FromResult(httpResponse);
        }

        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="token">jwt身份令牌</param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> PostAsync(string url, string content, string token = null)
        {
            var httpResponse = new HttpResponse<T>();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, HttpRequestHeaderConst.ContentType.JSON);
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
                }
            }
            if (!string.IsNullOrEmpty(responseResult))
            {
                var contentJson = await responseResult.ToObjectAsync<T>();
                httpResponse.Data = contentJson;
                return httpResponse;
            }
            return await Task.FromResult(httpResponse);
        }
    }
}
