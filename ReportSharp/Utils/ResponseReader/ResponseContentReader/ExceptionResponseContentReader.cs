using System;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.ResponseReader.ResponseContentReader
{
    public class ExceptionResponseContentReader : IResponseContentReader
    {
        public virtual string Read(HttpContext context, Exception exception)
        {
            return exception.ToString();
        }
    }
}