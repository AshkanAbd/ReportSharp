using System;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Utils.ResponseReader.ResponseContentReader
{
    public class FileResponseContentReader : IResponseContentReader
    {
        public virtual string Read(HttpContext context, Exception exception)
        {
            return $"File: {context.Response.ContentType} | {context.Response.ContentLength} bytes";
        }
    }
}