using DurableFunction.Models;
using DurableFunction.Service;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DurableFunction
{
    public static class Starter
    {
        [FunctionName("QueueCatcher")]
        public static async Task RunAsync(
            [QueueTrigger("request", Connection = "AzureWebJobsStorage")] QueueItem queueItem,
            [OrchestrationClient] DurableOrchestrationClient starter,
            ILogger log)
        {           
            var orchestrationId = await starter.StartNewAsync(Orchestration.ProcessName, queueItem);          
        }
        [FunctionName("Approve")]
        public static async Task<HttpResponseMessage> Approval(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Approve/{id}")]
            HttpRequestMessage req, [OrchestrationClient] DurableOrchestrationClient client, string id, ILogger log)
        {
            var result = req.RequestUri.ParseQueryString()["result"];
            await client.RaiseEventAsync(id, Orchestration.ApprovalResultName, result);

            log.LogWarning($"External event triggered by request: {req}");

            return req.CreateResponse(HttpStatusCode.OK);
        }


    }
}
