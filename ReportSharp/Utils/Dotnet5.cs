using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace ReportSharp.Utils
{
    public static class Dotnet5
    {
        public static bool HasJsonContentType(HttpRequest request)
        {
            return HasJsonContentType(request, out var _);
        }

        private static bool HasJsonContentType(HttpRequest request, out StringSegment charset)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            MediaTypeHeaderValue parsedValue;
            if (!MediaTypeHeaderValue.TryParse((StringSegment) request.ContentType, out parsedValue)) {
                charset = StringSegment.Empty;
                return false;
            }

            if (parsedValue.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase)) {
                charset = parsedValue.Charset;
                return true;
            }

            if (parsedValue.Suffix.Equals("json", StringComparison.OrdinalIgnoreCase)) {
                charset = parsedValue.Charset;
                return true;
            }

            charset = StringSegment.Empty;
            return false;
        }
    }
}