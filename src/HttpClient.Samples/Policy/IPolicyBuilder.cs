using System.Net.Http;
using Polly;

namespace HttpClient.Samples.Policy
{
    public interface IPolicyBuilder
    {
        IAsyncPolicy<HttpResponseMessage> BuildGenericPolicy(PolicyOption policyOption);
    }
}
    