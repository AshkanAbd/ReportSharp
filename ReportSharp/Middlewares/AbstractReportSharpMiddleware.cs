using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReportSharp.Configs;
using ReportSharp.Models;
using ReportSharp.Services.ReportSharpService;
using ReportSharp.Utils.RequestReader;
using ReportSharp.Utils.RequestReader.RequestBodyReader;
using ReportSharp.Utils.ResponseReader;
using ReportSharp.Utils.ResponseReader.ResponseContentReader;
using static ReportSharp.Utils.Dotnet5;

namespace ReportSharp.Middlewares
{
    public abstract class AbstractReportSharpMiddleware : IReportSharpMiddleware
    {
        public AbstractReportSharpMiddleware(RequestDelegate next = null)
        {
            Next = next;
        }

        public RequestDelegate Next { get; }
        public IReportSharpService ReportSharpService { get; set; }

        public abstract Task OnExceptionAsync(HttpContext context, Exception exception);

        public abstract Task BeforeExecutingAsync(HttpContext context);

        public abstract Task OnExecutedAsync(HttpContext context);

        public virtual async Task InvokeAsync(HttpContext context, IReportSharpService reportSharpService)
        {
            var reportSharpConfig = GetReportSharpConfig(context.RequestServices);

            if (context.Request.Path.Value == null || !context.Request.Path.HasValue ||
                !context.Request.Path.Value.ToLower().StartsWith(reportSharpConfig.WatchdogPrefix.ToLower())) {
                await Next(context);
                return;
            }

            context.Request.EnableBuffering();
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            var reportSharpRequest = await ReadRequest(context);

            context.Request.Body.Seek(0, SeekOrigin.Begin);

            ReportSharpService = reportSharpService;
            ReportSharpService.SetCurrentLog(reportSharpRequest);

            await BeforeExecutingAsync(context);
            var originalBody = context.Response.Body;

            try {
                await using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;

                await Next(context);

                if (HasExceptionHandlerFeature(context)) {
                    context.Response.Body = originalBody;

                    await ReadResponse(context, GetExceptionHandlerFeature(context));

                    await OnExceptionAsync(context, GetExceptionHandlerFeature(context));
                }
                else {
                    memoryStream.Position = 0;

                    await ReadResponse(context, null);

                    memoryStream.Position = 0;

                    await OnExecutedAsync(context);

                    await memoryStream.CopyToAsync(originalBody);
                }
            }
            catch (Exception e) {
                context.Response.Body = originalBody;

                await ReadResponse(context, e);

                await OnExceptionAsync(context, e);
                throw;
            }
            finally {
                context.Response.Body = originalBody;
            }
        }

        protected virtual async Task ReadResponse(HttpContext context, Exception exception)
        {
            var responseReader = new ResponseReader(context, exception);

            if (exception != null)
                responseReader.SetResponseContentReader(new ExceptionResponseContentReader());
            else if (context.Response.ContentType == null)
                responseReader.SetResponseContentReader(new JsonResponseContentReader());
            else if (context.Response.ContentType.ToLower().Contains("charset=UTF-8".ToLower()))
                responseReader.SetResponseContentReader(new JsonResponseContentReader());
            else
                responseReader.SetResponseContentReader(new FileResponseContentReader());

            var reportSharpResponse = await responseReader.Read();

            reportSharpResponse.RequestId = ReportSharpService.GetCurrentLog().Id;
            reportSharpResponse.ReportSharpRequest = ReportSharpService.GetCurrentLog();
            ReportSharpService.GetCurrentLog().ReportSharpResponse = reportSharpResponse;
        }

        protected virtual async Task<ReportSharpRequest> ReadRequest(HttpContext context)
        {
            var requestReader = new RequestReader(context);

            if (context.Request.HasFormContentType)
                requestReader.SetRequestBodyReader(new FormDataRequestBodyReader());
            else if (HasJsonContentType(context.Request))
                requestReader.SetRequestBodyReader(new JsonAsyncRequestBodyReader());

            return await requestReader.Read();
        }

        protected virtual bool HasExceptionHandlerFeature(HttpContext context)
        {
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

            return exceptionHandlerFeature != null;
        }

        protected virtual Exception GetExceptionHandlerFeature(HttpContext context)
        {
            if (HasExceptionHandlerFeature(context)) return context.Features.Get<IExceptionHandlerFeature>().Error;

            return null;
        }

        protected virtual ReportSharpConfig GetReportSharpConfig(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetService<IOptions<ReportSharpConfig>>();
            return options == null ? new ReportSharpConfig() : options.Value;
        }
    }
}