using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.RequestReader.RequestBodyReader
{
    public interface IAsyncRequestBodyReader : IRequestBodyReader
    {
        string IRequestBodyReader.Read(HttpContext context)
        {
            var result = new TaskFactory<Task<string>>()
                .StartNew(async () => await ReadAsync(context))
                .Unwrap()
                .GetAwaiter()
                .GetResult();
            return result;
        }


        public Task<string> ReadAsync(HttpContext context);
    }
}