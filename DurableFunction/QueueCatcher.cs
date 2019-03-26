using DurableFunction.Models;
using DurableFunction.Service;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace DurableFunction
{
    public static class QueueCatcher
    {
        [FunctionName("QueueCatcher")]
        public static async Task RunAsync(
            [QueueTrigger("request", Connection = "AzureWebJobsStorage")] QueueItem queueItem,
             ExecutionContext appContext, [OrchestrationClient] DurableOrchestrationClient starter,
            ILogger log)
        {
            var config = GetConfig(appContext);
            var table = new TableStorage(config["AzureWebJobsStorage"], "emails");
            log.LogInformation($"C# Queue trigger function processed: {queueItem.Id}");

            var item = await table.GetItem("person", queueItem.Id);

            log.LogInformation($"Position from email {item.Email}");
            var orchestrationId = await starter.StartNewAsync("SendEmail", item);          
        }


        private static IConfigurationRoot GetConfig(ExecutionContext context)
        {
            return new ConfigurationBuilder()
                 .SetBasePath(context.FunctionAppDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();
        }
    }
}
