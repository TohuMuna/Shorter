namespace Shorter.Providers
{
    using System.Threading.Tasks;

    using Shorter.Core.Dto;

    public abstract class BaseProvider : IProvider
    {
        public abstract Task<ShortCode> GenerateShortCode(string url);

        public abstract Task<ShortCode> GetUrlFromShortCode(string code);

        protected abstract Task<string> GetCode();

        protected abstract Task<ShortCode> IsAlreadyShortened(string url);
    }
}
