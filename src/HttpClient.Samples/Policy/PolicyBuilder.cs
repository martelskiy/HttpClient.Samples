using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace HttpClient.Samples.Policy
{
    public class PolicyBuilder : IPolicyBuilder
    {
        private readonly ILogger<PolicyBuilder> _logger;

        public PolicyBuilder(ILogger<PolicyBuilder> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IAsyncPolicy<HttpResponseMessage> BuildGenericPolicy(PolicyOption policyOption)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(policyOption.AmountOfRetries,
                    i => TimeSpan.FromSeconds(Math.Pow(2, i)),
                    (result, retryCount) =>
                    {
                        _logger.LogError($"Exception occured. Will retry in: {retryCount.Seconds} sec. " +
                                         $"\n {result.Exception.StackTrace} " +
                                         $"\n Exception message : {result.Exception.Message}", result);
                    });
        }
    }
}
