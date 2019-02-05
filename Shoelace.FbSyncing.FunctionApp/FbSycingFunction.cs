using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Shoelace.AzureFunctionDIHelper;
using Shoelace.FbSyncing.FunctionApp.DependencyInjection;

namespace Shoelace.FbSyncing.FunctionApp
{
    public static class FbSycingFunction
    {
        private static HttpClient _httpClient;
        private static ITopicClient _topicClient;


        private static readonly IServiceProvider ServiceContainer = new ContainerBuilder()
            .RegisterModule(new FbSyncingAppModule())
            .Build();
        //[Singleton(Mode = SingletonMode.Listener)]
        [FunctionName("FbSycingFunction")]
        public static async Task Run([ServiceBusTrigger("shoelace-facebook-sync-topic", "sub-sync", Connection = "ServiceBusConnection")]string receivedMsg, ILogger log)
        {
            try
            {
                //_httpClient = ServiceContainer.GetService<HttpClient>();
                //_topicClient = ServiceContainer.GetService<ITopicClient>();

                log.LogWarning($"=> Received Message From topic 'shoelace-facebook-sync-topic'. MsgBody= {receivedMsg}");
                //Thread.Sleep(TimeSpan.FromSeconds(3));
                //var request = new HttpRequestMessage(HttpMethod.Get, new Uri(""));
                //request.Headers.Add("X-Shopify-Access-Token", shopifyRequest.User.AccessToken);
                //var response = await _httpClient.SendAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
