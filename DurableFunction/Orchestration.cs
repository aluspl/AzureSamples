using DurableFunction.Models;
using Microsoft.Azure.WebJobs;
using System.Threading;
using System.Threading.Tasks;

namespace DurableFunction
{
    public static class Orchestration
    {
        public const string ApprovalResultName = "ApprovalResult";

        public const string ProcessName = "SendEmailProcess";

        [FunctionName(ProcessName)]
        public static async Task<object> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var item = context.GetInput<QueueItem>();
            var email = await context.CallActivityAsync<string>(Activity.GetEmailName, item.Id);
            await context.CallActivityAsync(Activity.SendEmailName, 
                new Item { Email=email, Id= context.InstanceId });
            var response = new ApprovalResult()
            {
                Email = email
            };
            using (var timeoutCts = new CancellationTokenSource())
            {
                var expiration = context.CurrentUtcDateTime.AddSeconds(120);
                var timeoutTask = context.CreateTimer(expiration, timeoutCts.Token);
                var responseTask = context.WaitForExternalEvent<string>(ApprovalResultName);

                var winner = await Task.WhenAny(responseTask, timeoutTask);
                if (winner == responseTask)
                {
                    response.Result = "Success";
                    timeoutCts.Cancel();
                }
                else
                {
                    response.Result = "Failed";
                    timeoutCts.Cancel();
                }
            }
            await context.CallActivityAsync(Activity.PostResultName, response); ;

            return item;
        }
    }
}