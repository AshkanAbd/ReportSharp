using System;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.ResponseReader.ResponseContentReader
{
    public interface IResponseContentReader
    {
        public string Read(HttpContext context, Exception exception);
    }
}