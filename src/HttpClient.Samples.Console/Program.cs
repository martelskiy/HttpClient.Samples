using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using HttpClient.Samples.CustomHttpClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HttpClient.Samples.Console
{
    public class Program
    {
        static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => services.AddCustomHttpClient("http://dummyurl"))
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .Build();

            var customHttpClient = host.Services.GetRequiredService<ICustomHttpClient>();

            await customHttpClient.GetAsync<Response>("/");

            await host.RunAsync();
        }
    }
}
