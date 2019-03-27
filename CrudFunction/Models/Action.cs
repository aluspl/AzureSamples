using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace lifelike.CrudFunction
{
    public class Action
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class ActionEntity : TableEntity
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }  
    public static class Mappings
    {
        public static ActionEntity ToEntity(this Action todo)
        {
            return new ActionEntity()
            {
                PartitionKey = "ACTION",
                RowKey = todo.Id,
                Name = todo.Name,
                Status = todo.Status,
            };
        }

        public static Action ToAction(this ActionEntity todo)
        {
            return new Action()
            {
                Id = todo.RowKey,
                Name = todo.Name,
                Status = todo.Status,
            };
        }
    }
}
