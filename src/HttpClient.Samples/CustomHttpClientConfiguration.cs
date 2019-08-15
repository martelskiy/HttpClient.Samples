using System;
using System.Collections.Generic;
using HttpClient.Samples.CustomHttpClient;
using HttpClient.Samples.Policy;
using Microsoft.Extensions.DependencyInjection;

namespace HttpClient.Samples
{
    public static class CustomHttpClientConfiguration
    {
        public static void AddGenericHttpClient(this IServiceCollection services, string baseAddress)
        {
            AddGenericHttpClient(services, baseAddress, null, new PolicyOption());
        }

        public static void AddGenericHttpClient(this IServiceCollection services, string baseAddress, KeyValuePair<string, string>[] headers)
        {
            AddGenericHttpClient(services, baseAddress, headers, new PolicyOption());
        }

        public static void AddGenericHttpClient(this IServiceCollection services, string baseAddress, KeyValuePair<string, string>[] headers, PolicyOption policyOption)
        {
            services.AddSingleton<IPolicyBuilder, PolicyBuilder>();

            services
                .AddHttpClient<ICustomHttpClient, CustomHttpClient.CustomHttpClient>(client =>
                {
                    client.BaseAddress = new Uri(baseAddress);

                    AddDefaultRequestHeaders(headers, client);
                })
                .AddPolicyHandler((serviceProvider, httpRequestMessage) =>
                {
                    var policyBuilder = serviceProvider.GetRequiredService<IPolicyBuilder>();

                    return policyBuilder.BuildGenericPolicy(policyOption);
                });
        }

        private static void AddDefaultRequestHeaders(KeyValuePair<string, string>[] headers, System.Net.Http.HttpClient client)
        {
            if (headers == null) return;

            foreach (var header in headers)
            {
                var result = client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);

                if (!result) throw new ArgumentException($"Invalid header {header.Key}");
            }
        }
    }
}
