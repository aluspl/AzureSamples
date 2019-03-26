using lifelike.CrudFunction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrudFunction
{
    public static class CRUD
    {
        [FunctionName("Get")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Table("Actions", Connection = "AzureWebJobsStorage")] CloudTable actionTable,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var query = new TableQuery<ActionEntity>();
            var segment = await actionTable.ExecuteQuerySegmentedAsync(query, null);
            return new OkObjectResult(segment.Select(Mappings.ToAction));
        }
        [FunctionName("GetAction")]
        public static async Task<IActionResult> RunById(
         [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{id}")] HttpRequest req,
        [Table("Actions", Connection = "AzureWebJobsStorage")] CloudTable actionTable, ILogger log,
        string id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (actionTable == null)
            {
                return new NotFoundResult();
            }
            var operation = TableOperation.Retrieve("*", id);
            var result = await actionTable.ExecuteAsync(operation);
            return new OkObjectResult(((ActionEntity)result.Result).ToAction());
        }
        [FunctionName("Delete")]
        public static async Task<IActionResult> DeleteAction(
         [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "{id}")] HttpRequest req,
         [Table("Actions", Connection = "AzureWebJobsStorage")] CloudTable action,
         string id)
        {
            var deleteOperation = TableOperation.Delete(new ActionEntity() { PartitionKey = "ACTION", RowKey = id, ETag = "*" });

            var deleteResult = await action.ExecuteAsync(deleteOperation);

            return new OkResult();
        }
        [FunctionName("Create")]
        public static async Task<IActionResult> CreateAction(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route =  null)] HttpRequest req,
           [Table("Actions", Connection = "AzureWebJobsStorage")] CloudTable action)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Action>(requestBody);
            var operation = TableOperation.Insert(data.ToEntity());
            await action.ExecuteAsync(operation);

            return new OkResult();
        }


        [FunctionName("Update")]
        public static async Task<IActionResult> UpdateAction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "{id}")] HttpRequest req,
            [Table("Actions", Connection = "AzureWebJobsStorage")] CloudTable action, string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Action>(requestBody);
            var findOperation = TableOperation.Retrieve<ActionEntity>("ACTION", id);
            var findResult = await action.ExecuteAsync(findOperation);
            if (findResult.Result == null)
            {
                return new NotFoundResult();
            }
            var updated = (ActionEntity)findResult.Result;
            updated.Name = data.Name;
            updated.Status = data.Status;
            var replaceOperation = TableOperation.Replace(updated);
            await action.ExecuteAsync(replaceOperation);

            return new OkResult();
        }
    }

}
