using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using QueueDBFunction.Models;
using System;

namespace QueueDBFunction
{
    public static class DBFunction
    {
        const string QueueName = "task";
        const string TopicName = "request";
        const string SubscriptionName = "db";

        [FunctionName("DBFunctionQueue")]
        public static void RunDBQueue([ServiceBusTrigger(QueueName, Connection = "SB")]string json,
          [Table("DB1", Connection = "AzureWebJobsStorage")] CloudTable table,
          ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {json}");
                var message = JsonConvert.DeserializeObject<QueueMsg>(json);

                AddItem(table, message);
            }
            catch (Exception e)
            {
                log.LogError(e, "DBFunctionQueue");
            }            
        }

        private static void AddItem(CloudTable table, QueueMsg message)
        {
            if (message.Type == "DB")
            {
                var item = new DBItemEntity()
                {
                    Body = message.Body,
                    PartitionKey = "DB"
                };
                var operation = TableOperation.Insert(item);
                table.ExecuteAsync(operation).Wait();
            }          
        }

        [FunctionName("DBFunctionTopic")]
        public static void RunDBTopic([ServiceBusTrigger(TopicName,SubscriptionName, Connection = "SB")]string json,
          [Table("DB1", Connection = "AzureWebJobsStorage")] CloudTable table,
          ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus trigger trigger function processed message: {json}");
                var message = JsonConvert.DeserializeObject<QueueMsg>(json);
                AddItem(table, message);
            }
            catch (Exception e)
            {
                log.LogError(e, "DBFunctionTopic");
            }
        }
    }
}
