namespace Shorter.Providers.Azure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    using Newtonsoft.Json;

    using Shorter.Core.Dto;

    public class TableClient : ITableClient
    {
        private static readonly HashSet<string> CreatedTables = new HashSet<string>(); 
        private readonly CloudTableClient tableClient;
        
        public TableClient(CloudStorageAccount account)
        {
            this.tableClient = account.CreateCloudTableClient();
        }

        public async Task<T> Get<T>(string partitionKey, string rowKey) where T : IModel
        {
            var table = this.GetTable<T>();
            var op = TableOperation.Retrieve<AzureEntity>(partitionKey, rowKey);

            var entity = await table.ExecuteAsync(op);
            if (entity == null)
            {
                return default(T);
            }

            var model = (AzureEntity)entity.Result;
            return JsonConvert.DeserializeObject<T>(model.Body);
        }

        public Task Insert<T>(T model) where T : IModel
        {
            var table = this.GetTable<T>();
            var entity = new AzureEntity(model.Id, model.Id, JsonConvert.SerializeObject(model));

            var op = TableOperation.Insert(entity);
            return table.ExecuteAsync(op);
        }

        private CloudTable GetTable<T>()
        {
            var tableName = typeof(T).Name;
            var table = this.tableClient.GetTableReference(tableName);

            if (!CreatedTables.Contains(tableName))
            {
                table.CreateIfNotExists();
                CreatedTables.Add(tableName);
            }

            return table;
        }
    }
}