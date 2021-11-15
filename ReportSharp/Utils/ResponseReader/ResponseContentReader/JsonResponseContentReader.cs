using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.ResponseReader.ResponseContentReader
{
    public class JsonResponseContentReader : IAsyncResponseContentReader
    {
        public async Task<string> ReadAsync(HttpContext context, Exception exception)
        {
            using var reader = new StreamReader(context.Response.Body, Encoding.UTF8,
                false, 1024, true);
            return Regex.Unescape(await reader.ReadToEndAsync());
        }
    }
}