using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.DbContext;
using ReportSharp.Models;

namespace ReportSharp.Reporters.DatabaseReporter
{
    public class DatabaseReporter : IDataReporter, IExceptionReporter, IRequestReporter
    {
        public DatabaseReporter(IServiceScopeFactory scopeFactory)
        {
            ReportSharpDbContext = scopeFactory.CreateScope().ServiceProvider.GetService<ReportSharpDbContext>();
        }

        public ReportSharpDbContext ReportSharpDbContext { get; set; }

        public async Task ReportData(HttpContext httpContext, string tag, string data)
        {
            if (ReportSharpDbContext == null) return;

            await ReportSharpDbContext.AddAsync(new ReportSharpData {
                Tag = tag,
                Data = data
            });
            await ReportSharpDbContext.SaveChangesAsync();
        }

        public async Task ReportException(HttpContext httpContext, ReportSharpRequest request, Exception exception)
        {
            if (ReportSharpDbContext == null) return;

            await ReportSharpDbContext.AddAsync(request);
            await ReportSharpDbContext.SaveChangesAsync();
        }

        public async Task ReportRequest(HttpContext httpContext, ReportSharpRequest request)
        {
            if (ReportSharpDbContext == null) return;

            await ReportSharpDbContext.AddAsync(request);
            await ReportSharpDbContext.SaveChangesAsync();
        }
    }
}