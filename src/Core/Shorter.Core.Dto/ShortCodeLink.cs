namespace Shorter.Core.Dto
{
    public class ShortCodeLink : IModel
    {
        public string Id { get; set; }
        public string ShortCode { get; set; }
        public string Url { get; set; }
    }
}
