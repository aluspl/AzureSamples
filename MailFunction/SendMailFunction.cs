using MailFunction.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System;

namespace MailFunction
{
    public static class SendMailFunction
    {
        const string QueueName = "task";
        const string TopicName = "request";
        const string SubscriptionName = "mail";

        [FunctionName("MailQueue")]
        [return: ServiceBus(QueueName, Connection = "SB")]
        public static string RunMailQueue(
         [ServiceBusTrigger(QueueName, Connection = "SB")]string json,
         [SendGrid(ApiKey = "SendGridKey")] out SendGridMessage message,
         ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {json}");
                var request = JsonConvert.DeserializeObject<QueueMsg>(json);
                if (request.Type == "Mail")
                {
                    message = SendEmail(request);
                    return JsonConvert.SerializeObject(new QueueMsg { Type = "DB", Body = request.Body });
                }
                else
                {
                    message = null;
                    return null;
                }
            }
            catch (Exception e)
            {
                log.LogError(e, "MailQueue");
                message = null;
                return null;
            }

        }


        [FunctionName("MailTopic")]
        [return: ServiceBus(TopicName, Connection = "SB")]
        public static string RunMailTopic(
          [ServiceBusTrigger(TopicName, SubscriptionName, Connection = "SB")]string json,
          [SendGrid(ApiKey = "SendGridKey")] out SendGridMessage message,
          ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus topic trigger function processed message: {json}");
                var request = JsonConvert.DeserializeObject<QueueMsg>(json);
                if (request.Type == "Mail")
                {
                    message = SendEmail(request);
                    return JsonConvert.SerializeObject(new QueueMsg { Type = "DB", Body = request.Body });
                }
                else
                {
                    message = null;
                    return null;
                }
            }
            catch (Exception e)
            {
                log.LogError(e, "MailTopic");
                message = null;
                return null;
            }
        }

        private static SendGridMessage SendEmail(QueueMsg request)
        {
            SendGridMessage message;
            var approverEmail = new EmailAddress(Environment.GetEnvironmentVariable("ReceiverEmail"));
            var senderEmail = new EmailAddress(Environment.GetEnvironmentVariable("SenderEmail"));
            var subject = "Sender Test";

            var body = $"{request.Body}";
            message = MailHelper.CreateSingleEmail(senderEmail, approverEmail, subject, "", body);
            return message;
        }
    }
}
