using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReportSharp.Models;
using ReportSharp.Utils.RequestReader.RequestBodyReader;

namespace ReportSharp.Utils.RequestReader
{
    public class RequestReader
    {
        private IRequestBodyReader _requestBodyReader;

        public RequestReader(HttpContext context)
        {
            Context = context;
        }

        public HttpContext Context { get; }

        public virtual async Task<ReportSharpRequest> Read()
        {
            var reportSharpRequest = new ReportSharpRequest {
                Url = Context.Request.Path,
                Verb = Context.Request.Method,
                Ip = Context.Request.Headers["X-Forwarded-For"].FirstOrDefault(),
                Headers = ReadHeaders(),
                QueryParameters = ReadQueries(),
                Body = await ReadBody()
            };

            return reportSharpRequest;
        }

        public virtual string ReadHeaders()
        {
            return string.Join(",\n", Context.Request.Headers
                .Select(x => $"{x.Key} => {x.Value}")
                .ToList());
        }

        public virtual string ReadQueries()
        {
            return string.Join(",\n", Context.Request.Query
                .Select(x => $"{x.Key} => {x.Value}")
                .ToList());
        }

        public virtual async Task<string> ReadBody()
        {
            if (_requestBodyReader is IAsyncRequestBodyReader asyncRequestBodyReader)
                return await asyncRequestBodyReader.ReadAsync(Context);

            return _requestBodyReader?.Read(Context);
        }

        public virtual void SetRequestBodyReader(IRequestBodyReader requestBodyReader)
        {
            _requestBodyReader = requestBodyReader;
        }
    }
}