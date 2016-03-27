using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CB.Web.WebServices
{
    public static class Http
    {
        #region Methods
        public static async Task<TResult> DeleteAsync<TResult>(string url)
            => await DoAsync<TResult>(async client => await client.DeleteAsync(url));

        public static async Task<TResult> DeleteAsync<TResult>(string url, int id)
            => await DeleteAsync<TResult>($"{url}?id={id}");

        public static async Task<TResult> DoAsync<TResult>(
            Func<HttpClient, Task<HttpResponseMessage>> getResponseFunc)
        {
            using (var client = new HttpClient())
            {
                var response = await getResponseFunc(client);
                return await GetContentValueAsync<TResult>(response.Content);
            }
        }

        public static async Task<TResult> GetAsync<TResult>(string url)
            => await DoAsync<TResult>(async client => await client.GetAsync(url));

        public static async Task<TResult> GetAsync<TResult>(string url, int id)
            => await GetAsync<TResult>($"url?id={id}");

        public static async Task<TResult> PostAsync<TParam, TResult>(string url, TParam param)
            => await DoAsync<TResult>(async client => await client.PostAsJsonAsync(url, param));

        public static async Task<TResult> PutAsync<TParam, TResult>(string url, TParam param)
            => await DoAsync<TResult>(async client => await client.PutAsJsonAsync(url, param));
        #endregion


        #region Implementation
        private static async Task<TResult> GetContentValueAsync<TResult>(HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(stringContent);
        }
        #endregion
    }
}