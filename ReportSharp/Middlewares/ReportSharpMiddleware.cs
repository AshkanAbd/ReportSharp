using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Reporters;

namespace ReportSharp.Middlewares
{
    public class ReportSharpMiddleware : AbstractReportSharpMiddleware
    {
        public ReportSharpMiddleware(RequestDelegate next = null) : base(next)
        {
        }

        public override Task OnExceptionAsync(HttpContext context, Exception exception)
        {
            var reportSharpRequest = ReportSharpService.GetRequest();

            var reporters = context.RequestServices.GetServices<IExceptionReporter>();
            foreach (var reporter in reporters) reporter.ReportException(context, reportSharpRequest, exception);

            return Task.CompletedTask;
        }

        public override Task BeforeExecutingAsync(HttpContext context)
        {
            return Task.CompletedTask;
        }

        public override Task OnExecutedAsync(HttpContext context)
        {
            var reportSharpRequest = ReportSharpService.GetRequest();

            var reporters = context.RequestServices.GetServices<IRequestReporter>();
            foreach (var reporter in reporters) reporter.ReportRequest(context, reportSharpRequest);

            return Task.CompletedTask;
        }
    }
}