using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.RequestReader.RequestBodyReader
{
    public class JsonAsyncRequestBodyReader : IAsyncRequestBodyReader
    {
        public async Task<string> ReadAsync(HttpContext context)
        {
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8,
                false, 1024, true);
            return Regex.Unescape(await reader.ReadToEndAsync());
        }
    }
}