using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpClient.Samples.CustomHttpClient
{
    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public CustomHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<T> PostAsync<T>(string requestUri, string jsonContent, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(requestUri, new StringContent(jsonContent), cancellationToken);

            return await ProcessResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string requestUri, string jsonContent, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsync(requestUri, new StringContent(jsonContent), cancellationToken);

            return await ProcessResponse<T>(response);
        }

        public async Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(requestUri, cancellationToken);

            return await ProcessResponse<T>(response);
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            using (var responseContentStream = await response.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(responseContentStream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();

                return serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
