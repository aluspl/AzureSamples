using DurableFunction.Service;
using Microsoft.Azure.WebJobs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DurableFunction
{
    public static class SendEmail
    {
        [FunctionName("O_SendEmail")]
        public static async Task<object> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var item = context.GetInput<ItemEntity>();
            var code = await context.CallActivityAsync<string>("A_SendEmail", item.Email);
            using (var timeoutCts = new CancellationTokenSource())
            {
                // The user has 90 seconds to respond with the code they received in the SMS message.
                DateTime expiration = context.CurrentUtcDateTime.AddSeconds(120);
                Task timeoutTask = context.CreateTimer(expiration, timeoutCts.Token);

                bool authorized = false;
                for (int retryCount = 0; retryCount <= 3; retryCount++)
                {
                    Task<string> challengeResponseTask =
                        context.WaitForExternalEvent<string>("SmsChallengeResponse");

                    Task winner = await Task.WhenAny(challengeResponseTask, timeoutTask);
                    if (winner == challengeResponseTask)
                    {
                        // We got back a response! Compare it to the challenge code.
                        if (challengeResponseTask.Result == code)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }
            }
            return item;
        }
    }
}