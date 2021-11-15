using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReportSharp.Configs;
using ReportSharp.Exceptions;
using ReportSharp.Middlewares;

namespace ReportSharp.Builder.ReportSharpConfigureBuilder
{
    public class ReportSharpConfigureBuilder : IReportSharpConfigureBuilder
    {
        public ReportSharpConfigureBuilder(IApplicationBuilder app)
        {
            App = app;
        }

        public IApplicationBuilder App { get; }

        public IReportSharpConfigureBuilder UseApis()
        {
            var options = App.ApplicationServices.GetService<IOptions<ReportSharpConfig>>();
            var reportSharpConfig = options == null ? new ReportSharpConfig() : options.Value;

            while (reportSharpConfig.ApiPrefix.EndsWith("/"))
                reportSharpConfig.ApiPrefix = reportSharpConfig.ApiPrefix
                    .Substring(0, reportSharpConfig.ApiPrefix.Length - 1);

            while (!reportSharpConfig.ApiPrefix.StartsWith("/"))
                reportSharpConfig.ApiPrefix = $"/{reportSharpConfig.ApiPrefix}";

            App.MapWhen(x => x.Request.Path.HasValue
                             && x.Request.Path.Value!.StartsWith(reportSharpConfig.ApiPrefix), builder => {
                builder.UseMvc(routes => {
                    routes.MapRoute("Index requests",
                        reportSharpConfig.ApiPrefix + "/request",
                        new {
                            controller = "Request",
                            action = "Index"
                        }
                    ).MapRoute("Show request",
                        reportSharpConfig.ApiPrefix + "/request/{id}",
                        new {
                            controller = "Request",
                            action = "Show"
                        }
                    ).MapRoute("Index exceptions",
                        reportSharpConfig.ApiPrefix + "/exception",
                        new {
                            controller = "Exception",
                            action = "Index"
                        }
                    ).MapRoute("Show exception",
                        reportSharpConfig.ApiPrefix + "/exception/{id}",
                        new {
                            controller = "Exception",
                            action = "Show"
                        }
                    ).MapRoute("Index data",
                        reportSharpConfig.ApiPrefix + "/data",
                        new {
                            controller = "Data",
                            action = "Index"
                        }
                    ).MapRoute("Show data",
                        reportSharpConfig.ApiPrefix + "/data/{id}",
                        new {
                            controller = "Data",
                            action = "Show"
                        }
                    );
                });
            });

            return this;
        }

        public IReportSharpConfigureBuilder UseReportSharpMiddleware<T>() where T : IReportSharpMiddleware
        {
            App.UseMiddleware<T>();

            return this;
        }

        public IReportSharpConfigureBuilder UseReportSharpMiddleware(Type type)
        {
            if (!typeof(IReportSharpMiddleware).IsAssignableFrom(type))
                throw new InvalidTypeException(type, "IReportSharp middleware");

            App.UseMiddleware(type);

            return this;
        }
    }
}