namespace Shorter.Providers
{
    using System.Threading.Tasks;

    using Shorter.Core.Dto;

    public interface IProvider
    {
        Task<ShortCode> GenerateShortCode(string url);

        Task<ShortCode> GetUrlFromShortCode(string code);
    }
}
