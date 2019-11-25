using ComplaintLoggingSystem.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintLoggingSystem.Services
{
    public class ServiceAgent
    {
        IHttpClientFactory _httpClientFactory;
        HttpClient _httpClient;
        IHttpContextAccessor _httpContextAccessor;

        public ServiceAgent(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, string clientName = "")
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            switch (clientName)
            {
                case UserConstants.CORELIBRARYHTTPCLIENT:
                    _httpClient = GetHttpClient();
                    break;

                default:
                    _httpClient = GetHttpClient();
                    break;

            }

        }

        public async Task<T> GetData<T>(string Url)
        {
            if (Url.Contains("?"))
            {
                if (Url.Contains("&"))
                {
                    Url = Url.Split(new string[] { "?" }, StringSplitOptions.None)[0] + "?" + Url.Split(new string[] { "?" }, StringSplitOptions.None)[1].Split(new string[] { "&" }, StringSplitOptions.None).Where(n => n[n.Length - 1] != '=').Aggregate((i, j) => i + "&" + j);
                }
                else
                {
                    Url = Url[Url.Length - 1] != '=' ? Url : Url.Split(new string[] { "?" }, StringSplitOptions.None)[0];
                }
            }

            var response = await _httpClient.GetAsync(Url);
            return await ParseResponse<T>(response);
        }

        public async Task<T> GetDataWithContent<T>(string Url)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, Url);
            httpRequest.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(httpRequest);
            return await ParseResponse<T>(response);
        }

        public async Task<T> PostData<T>(string Url, object postObject)
        {
            var postJsonObject = JsonConvert.SerializeObject(postObject);
            var buffer = System.Text.Encoding.UTF8.GetBytes(postJsonObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync(Url, byteContent);

            return await ParseResponse<T>(response);

        }

        public async Task<T> PutData<T>(string Url, object postObject)
        {
            var postJsonObject = JsonConvert.SerializeObject(postObject);
            var buffer = System.Text.Encoding.UTF8.GetBytes(postJsonObject);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PutAsync(Url, byteContent);

            return await ParseResponse<T>(response);

        }

        public async Task<T> DeleteData<T>(string Url)
        {
            var response = await _httpClient.DeleteAsync(Url);
            return await ParseResponse<T>(response);
        }

        public async Task<Stream> GetStreamData<T>(string Url)
        {
            var response = await _httpClient.GetAsync(Url);
            return await ParseStreamResponse<Stream>(response);
        }

        public async Task<Stream> PostStreamData<T>(string Url, object postObject)
        {
            var postJsonObject = JsonConvert.SerializeObject(postObject);
            var buffer = System.Text.Encoding.UTF8.GetBytes(postJsonObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync(Url, byteContent);

            return await ParseStreamResponse<Stream>(response);

        }

        private static async Task<Stream> ParseStreamResponse<T>(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default(Stream);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringResult = await response.Content.ReadAsStreamAsync();
                return stringResult;
            }
            else
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                throw new Exception();
            }
        }

        public async Task<T> DeleteDataWithObj<T>(string Url, object delObject)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Url);
            request.Content = new StringContent(JsonConvert.SerializeObject(delObject), Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            return await ParseResponse<T>(response);
        }

        private static async Task<T> ParseResponse<T>(HttpResponseMessage response)
        {

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return default(T);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Endpoint " + response.RequestMessage.RequestUri.AbsoluteUri + " not found");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var serviceResponse = JsonConvert.DeserializeObject<T>(stringResult);
                return serviceResponse;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                var serviceResponse = JsonConvert.DeserializeObject<T>(stringResult);
                return serviceResponse;
            }
            else
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                try
                {
                    throw new Exception();
                }
                catch (JsonReaderException ex)
                {

                    throw new Exception("Internal Server Error");
                }
            }
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = UserConstants.CORELIBRARYHTTPCLIENT;
            var client = _httpClientFactory.CreateClient(httpClient);
            return client;
        }

       
    }
}
