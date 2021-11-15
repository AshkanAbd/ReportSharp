using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReportSharp.Models;
using ReportSharp.Utils.ResponseReader.ResponseContentReader;

namespace ReportSharp.Utils.ResponseReader
{
    public class ResponseReader
    {
        private IResponseContentReader _responseContentReader;

        public ResponseReader(HttpContext context, Exception exception)
        {
            Context = context;
            Exception = exception;
        }

        public HttpContext Context { get; }
        public Exception Exception { get; }

        public async Task<ReportSharpResponse> Read()
        {
            var reportSharpResponse = new ReportSharpResponse {
                StatusCode = Exception != null ? "500" : Context.Response.StatusCode.ToString(),
                Content = await ReadContent(),
                Headers = ReadHeaders()
            };

            return reportSharpResponse;
        }

        public async Task<string> ReadContent()
        {
            if (_responseContentReader is IAsyncResponseContentReader asyncResponseContentReader)
                return await asyncResponseContentReader.ReadAsync(Context, Exception);

            return _responseContentReader?.Read(Context, Exception);
        }

        public string ReadHeaders()
        {
            return string.Join(",\n", Context.Response.Headers
                .Select(x => $"{x.Key} => {x.Value}")
                .ToList());
        }

        public void SetResponseContentReader(IResponseContentReader responseContentReader)
        {
            _responseContentReader = responseContentReader;
        }
    }
}