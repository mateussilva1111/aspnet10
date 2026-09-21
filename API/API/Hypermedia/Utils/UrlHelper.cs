using Microsoft.AspNetCore.Mvc;

namespace API.Hypermedia.Utils
{
    public static class UrlHelper
    {
        private static readonly object _lock = new();

        public static string BuildBaseUrl(this IUrlHelper urlHelper, string routName, string path )
        {
            lock (_lock)
            {
                var url = urlHelper.Link(routName, new { controller = path }) ?? string.Empty;
                if (url == null) return string.Empty;
                url = url.Replace("api", path);
                return url.Replace("%2f", "/").TrimEnd('/');
            }
        }
    }
}
