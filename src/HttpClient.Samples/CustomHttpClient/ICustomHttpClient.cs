using System.Threading;
using System.Threading.Tasks;

namespace HttpClient.Samples.CustomHttpClient
{
    public interface ICustomHttpClient
    {
        Task<T> PostAsync<T>(string requestUri, string jsonContent, CancellationToken cancellationToken = default);
        Task<T> PutAsync<T>(string requestUri, string jsonContent, CancellationToken cancellationToken = default);
        Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken = default);
    }
}