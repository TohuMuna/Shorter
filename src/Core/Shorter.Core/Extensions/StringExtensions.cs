namespace Shorter.Core.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string ValidateUrl(this string url)
        {
            var builder = new UriBuilder(url);
            if (!builder.Host.Contains("."))
            {
                throw new Exception("Invalid URL.");
            }

            url = builder.Uri.ToString();
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        public static string UrlToBase64(this string url)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(url);
            var base64 = System.Convert.ToBase64String(bytes);
            return base64.Replace('/', '_');
        }
    }
}
