using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.ResponseReader.ResponseContentReader
{
    public interface IAsyncResponseContentReader : IResponseContentReader
    {
        string IResponseContentReader.Read(HttpContext context, Exception exception)
        {
            var result = new TaskFactory<Task<string>>()
                .StartNew(async () => await ReadAsync(context, exception))
                .Unwrap()
                .GetAwaiter()
                .GetResult();
            return result;
        }

        public Task<string> ReadAsync(HttpContext context, Exception exception);
    }
}