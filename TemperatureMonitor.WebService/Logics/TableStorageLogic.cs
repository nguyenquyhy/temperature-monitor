using Microsoft.Framework.ConfigurationModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TemperatureMonitor.WebService.Logics
{
    public class TableStorageLogic
    {
        private CloudStorageAccount account;

        public TableStorageLogic(BaseConfigurationSource configurationSource)
        {
            account = CloudStorageAccount.Parse(configurationSource.Data["AzureStorage_ConnectionString"]);
        }

        public async Task<T> GetAsync<T>(string tableName, string partitionKey, string rowKey) where T : class, ITableEntity
        {
            var table = await PrepareTableAsync(tableName);
            var operation = TableOperation.Retrieve(partitionKey, rowKey);
            var result = await table.ExecuteAsync(operation);

            return result.Result as T;
        }

        public async Task AddOrReplaceAsync<T>(string tableName, T entity) where T : ITableEntity
        {
            var table = await PrepareTableAsync(tableName);
            var operation = TableOperation.InsertOrReplace(entity);
            await table.ExecuteAsync(operation);
        }

        public async Task AddAsync<T>(string tableName, List<T> entities) where T : ITableEntity
        {
            var table = await PrepareTableAsync(tableName);
            var operation = new TableBatchOperation();
            foreach (var entity in entities)
            {
                operation.Add(TableOperation.Insert(entity));
            }
            await table.ExecuteBatchAsync(operation);
        }

        public async Task RemoveAsync<T>(string tableName, T entity) where T : ITableEntity
        {
            var table = await PrepareTableAsync(tableName);
            var operation = TableOperation.Delete(entity);
            await table.ExecuteAsync(operation);
        }

        public async Task RemoveAsync<T>(string tableName, List<T> entities) where T : ITableEntity
        {
            var table = await PrepareTableAsync(tableName);
            var operation = new TableBatchOperation();
            foreach (var entity in entities)
            {
                operation.Add(TableOperation.Delete(entity));
            }
            await table.ExecuteBatchAsync(operation);
        }

        private async Task<CloudTable> PrepareTableAsync(string tableName)
        {
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                Trace.TraceInformation(tableName + " is created!");
            }
            return table;
        }
    }
}