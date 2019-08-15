using System.Net.Http;
using Polly.Retry;

namespace HttpClient.Samples.Policy
{
    public interface IPolicyBuilder
    {
        RetryPolicy<HttpResponseMessage> BuildGenericPolicy(PolicyOption policyOption);
    }
}
