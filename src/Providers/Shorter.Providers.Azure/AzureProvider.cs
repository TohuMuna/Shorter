namespace Shorter.Providers.Azure
{
    using System;
    using System.Threading.Tasks;

    using Shorter.Core;
    using Shorter.Core.Dto;
    using Shorter.Core.Extensions;

    public class AzureProvider : BaseProvider
    {
        private readonly ITableClient tableClient;

        public AzureProvider(ITableClient tableClient)
        {
            this.tableClient = tableClient;
        }

        public override async Task<ShortCode> GenerateShortCode(string url)
        {
            var existing = await this.IsAlreadyShortened(url);
            if (existing != null)
            {
                return existing;
            }

            var code = await this.GetCode();

            var shortCode = new ShortCode { Id = code, Url = url, Created = DateTime.UtcNow };
            var shortCodeLink = new ShortCodeLink { Id = url.UrlToBase64(), ShortCode = code, Url = url };

            await Task.WhenAll(this.tableClient.Insert(shortCode), this.tableClient.Insert(shortCodeLink));

            return shortCode;
        }

        public override Task<ShortCode> GetUrlFromShortCode(string code)
        {
            return this.tableClient.Get<ShortCode>(code, code);
        }

        /// <summary>
        /// Generate a new short code and verify that it doesn't already exist in the system
        /// Continue creating short codes until we find a new one
        /// </summary>
        protected override async Task<string> GetCode()
        {
            var code = ShortCodeGenerator.Generate();
            var existing = await this.tableClient.Get<ShortCode>(code, code);
            while (existing != null)
            {
                code = ShortCodeGenerator.Generate();
                existing = await this.tableClient.Get<ShortCode>(code, code);
            }

            return code;
        }

        protected override async Task<ShortCode> IsAlreadyShortened(string url)
        {
            var baseLink = url.UrlToBase64();
            var link = await this.tableClient.Get<ShortCodeLink>(baseLink, baseLink);
            if (link == null)
            {
                return null;
            }

            return await this.tableClient.Get<ShortCode>(link.ShortCode, link.ShortCode);
        }
    }
}
