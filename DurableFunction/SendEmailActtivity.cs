using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using SendGrid.Helpers.Mail;

namespace DurableFunction
{
    public static class SendEmailActtivity
    {
        private static IWebProxy setting;

        [FunctionName("A_SendEmail")]
        public static async Task<string> Run(
            [ActivityTrigger] string email,
            ILogger log)
        {
            var rand = Guid.NewGuid().ToString();
            log.LogInformation($"Sending verification code {rand} to {email}.");

            var option = new SendGrid.SendGridClientOptions()
            {

            };
            var emailClient = new SendGrid.SendGridClient(option);
            var message = new SendGridMessage()
            {
                Subject = "Confirm email",
                M
            };
            await emailClient.SendEmailAsync(message);
            return rand;
        }
    }
}
