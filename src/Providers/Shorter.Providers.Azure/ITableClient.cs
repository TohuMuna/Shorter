namespace Shorter.Providers.Azure
{
    using System.Threading.Tasks;

    using Shorter.Core.Dto;

    public interface ITableClient
    {
        Task<T> Get<T>(string partitionKey, string rowKey) where T : IModel;

        Task Insert<T>(T model) where T : IModel;
    }
}