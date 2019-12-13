using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using YoShop.WeChat.Common;

namespace YoShop.WeChat
{
    public class WxHttpClient
    {
        private readonly HttpClient _httpClient;

        public WxHttpClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.BaseAddress = new Uri("https://api.weixin.qq.com");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
        }

        /// <summary>
        ///     获取请求JSON
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>JSON字符串</returns>
        public async Task<string> GetAsync(string url)
        {
            var result = await _httpClient.GetStringAsync(url);
            return result;
        }

        /// <summary>
        ///     GET提交请求，返回WxResult对象
        /// </summary>
        /// <typeparam name="T">WxResult对象</typeparam>
        /// <param name="url">请求地址</param>
        /// <returns>WxResult对象</returns>
        public async Task<T> GetAsync<T>(string url) where T : WxResult
        {
            var result = await GetAsync(url);
            var obj = JsonConvert.DeserializeObject<T>(result);
            if (obj != null)
                obj.DetailResult = result;
            return obj;
        }

        /// <summary>
        ///     GET提交请求，返回WxResult对象
        /// </summary>
        /// <typeparam name="T">WxResult对象</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="jsonConverts">Json转换器</param>
        /// <returns>WxResult对象</returns>
        public async Task<T> GetAsync<T>(string url, params JsonConverter[] jsonConverts) where T : WxResult
        {
            var result = await GetAsync(url);
            var obj = JsonConvert.DeserializeObject<T>(result, jsonConverts);
            if (obj != null)
                obj.DetailResult = result;
            return obj;
        }
    }
}
