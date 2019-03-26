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
using SendGrid;
using DurableFunction.Service;
using DurableFunction.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace DurableFunction
{
    public static class Activity
    {
        public const string SendEmailName = "SendEmail";
        public const string GetEmailName = "GetEmail";
        public const string PostResultName = "PostResult";

        [FunctionName(SendEmailName)]
        public static  void SendEmail([ActivityTrigger] Item email,
            [SendGrid(ApiKey = "SendGridKey")] out SendGridMessage message,
            ILogger log)
        {
            if (email.Email== null)
            {
                message = null;
                return;
            }
            var approverEmail = new EmailAddress(email.Email);
            var senderEmail = new EmailAddress(Environment.GetEnvironmentVariable("SenderEmail"));
            var subject = "A task is awaiting approval";

            var host = Environment.GetEnvironmentVariable("Host");
            var functionAddress = $"{host}/api/Approve/{email.Id}";
            var approvedLink = functionAddress + "?result=Approved";
            var body = $"<a href=\"{approvedLink}\">Approve</a>";
            log.Log(LogLevel.Information, $"Send {email.Id} to {email.Email} with Link {approvedLink}");
            message = MailHelper.CreateSingleEmail(senderEmail, approverEmail, subject, "", body);
        }
        [FunctionName(GetEmailName)]
        public static async Task<string> Run(
           [ActivityTrigger] string Id,
           [Table("emails", Connection = "AzureWebJobsStorage")] CloudTable actionTable,

           ExecutionContext appContext,
           ILogger log)
        {
            var query = TableOperation.Retrieve<ItemEntity>("person", Id);
            var item = await actionTable.ExecuteAsync(query);
            return ((ItemEntity)item.Result).Email;
        }
        [FunctionName(PostResultName)]
        public static async Task PostResultOnQueue([ActivityTrigger] ApprovalResult result,
          [Queue("result", Connection = "AzureWebJobsStorage")]
            CloudQueue queue)
        {        
            await queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(result)));
        }
    }
}
