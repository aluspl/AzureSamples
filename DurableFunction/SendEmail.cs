using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DurableFunction.Service;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableFunction
{
    public static class SendEmail
    {
        [FunctionName("SendEmail")]
        public static async Task<object> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var item = context.GetInput<ItemEntity>();

            return item;
        }
    }
} 