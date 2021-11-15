using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Middlewares
{
    public interface IReportSharpMiddleware
    {
        public Task OnExceptionAsync(HttpContext context, Exception exception);

        public Task BeforeExecutingAsync(HttpContext context);

        public Task OnExecutedAsync(HttpContext context);
    }
}