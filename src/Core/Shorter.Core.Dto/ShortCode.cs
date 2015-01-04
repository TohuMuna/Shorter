namespace Shorter.Core.Dto
{
    using System;

    public class ShortCode : IModel
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }
}
