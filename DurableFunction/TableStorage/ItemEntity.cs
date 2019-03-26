using Microsoft.WindowsAzure.Storage.Table;

namespace DurableFunction.Service
{
    public class ItemEntity : TableEntity
    {
        public string Email { get; set; }    
    }
}