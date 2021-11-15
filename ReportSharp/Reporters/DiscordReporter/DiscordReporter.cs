using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Models;
using ReportSharp.Services.DiscordService;

namespace ReportSharp.Reporters.DiscordReporter
{
    public class DiscordReporter : IDataReporter, IExceptionReporter, IRequestReporter
    {
        public DiscordReporter(IServiceProvider provider)
        {
            DiscordService = provider.GetService<IDiscordService>();
        }

        public IDiscordService DiscordService { get; set; }

        public async Task ReportData(HttpContext httpContext, string tag, string data)
        {
            await DiscordService.SendMessage($"#{tag}: {data}");
        }

        public async Task ReportException(HttpContext httpContext, ReportSharpRequest request, Exception exception)
        {
            if (DiscordService == null) return;

            await DiscordService.SendMessage(request.ToString());
            await DiscordService.SendMessage(request.ReportSharpResponse.ToString());
        }

        public async Task ReportRequest(HttpContext httpContext, ReportSharpRequest request)
        {
            if (DiscordService == null) return;

            await DiscordService.SendMessage(request.ToString());
            await DiscordService.SendMessage(request.ReportSharpResponse.ToString());
        }
    }
}