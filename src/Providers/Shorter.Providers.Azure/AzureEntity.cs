namespace Shorter.Providers.Azure
{
    using Microsoft.WindowsAzure.Storage.Table;

    public class AzureEntity : TableEntity
    {
        public AzureEntity(string partitionKey, string rowKey, string body)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;

            this.Body = body;
        }

        public AzureEntity()
        {
        }

        public string Body { get; set; }
    }
}
