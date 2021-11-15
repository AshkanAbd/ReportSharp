using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Builder.ReportSharpConfigureBuilder;
using ReportSharp.Builder.ReportSharpOptionsBuilder;
using ReportSharp.Services.ReportSharpService;

namespace ReportSharp.Extensions
{
    public static class ReportSharpExtension
    {
        public static IServiceCollection AddReportSharp(
            this IServiceCollection serviceCollection,
            Action<IReportSharpOptionsBuilder> options
        )
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var reportSharpOptionsBuilder = new ReportSharpOptionsBuilder(serviceCollection);
            options(reportSharpOptionsBuilder);

            serviceCollection.AddScoped<IReportSharpService, ReportSharpService>();

            return serviceCollection;
        }

        public static IApplicationBuilder UseReportSharp(this IApplicationBuilder app,
            Action<IReportSharpConfigureBuilder> configureBuilder)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (configureBuilder == null)
                throw new ArgumentNullException(nameof(configureBuilder));

            var configure = new ReportSharpConfigureBuilder(app);
            configureBuilder(configure);

            return app;
        }
    }
}