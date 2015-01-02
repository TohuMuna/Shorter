namespace Shorter.Providers.Azure
{
    using System;
    using System.Threading.Tasks;

    using Shorter.Core.Dto;

    public class AzureProvider : IProvider
    {
        public Task<ShortCode> GenerateShortCode(string url)
        {
            throw new NotImplementedException();
        }

        public Task<ShortCode> GetUrlFromShortCode(string shortCode)
        {
            throw new NotImplementedException();
        }
    }
}
