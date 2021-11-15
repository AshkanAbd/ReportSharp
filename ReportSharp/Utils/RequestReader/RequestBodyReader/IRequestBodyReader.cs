using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.RequestReader.RequestBodyReader
{
    public interface IRequestBodyReader
    {
        public string Read(HttpContext context);
    }
}