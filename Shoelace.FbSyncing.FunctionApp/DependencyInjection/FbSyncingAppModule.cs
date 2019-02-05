using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Shoelace.AzureFunctionDIHelper;

namespace Shoelace.FbSyncing.FunctionApp.DependencyInjection
{
    /// <summary>
    /// This represents the module entity for dependencies.
    /// </summary>
    public class FbSyncingAppModule : Module
    {
        private readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath("..\\")
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        /// <inheritdoc />
        public override void Load(IServiceCollection services)
        {

            var topicName = _configuration["TopicName"] ?? "shoelace-facebook-sync-topic";
            var serviceBusConnectionString = _configuration["ShopifyCachingServiceBusConnection"] ?? "Endpoint=sb://shopify-caching-servicebus-dev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=aofKl0WDfgro9zC55d5OZckRGvY1flwax6LkdNnf01o=";
            int.TryParse(_configuration["SbMsgReceiverTimeout"] ?? "5", out var sbMsgReceiverTimeout);

            var topicClient = new TopicClient(serviceBusConnectionString, topicName);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            services.AddSingleton<HttpClient>(httpClient)
                    .AddSingleton<ITopicClient>(topicClient);

        }
    }
}