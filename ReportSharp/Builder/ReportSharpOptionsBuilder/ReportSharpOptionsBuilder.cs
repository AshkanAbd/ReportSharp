using System;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Builder.ApiOptionsBuilder;
using ReportSharp.Builder.ConfigOptionsBuilder;
using ReportSharp.Builder.ReporterOptionsBuilder;
using ReportSharp.Reporters;

namespace ReportSharp.Builder.ReportSharpOptionsBuilder
{
    public class ReportSharpOptionsBuilder : IReportSharpOptionsBuilder
    {
        public ReportSharpOptionsBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; }

        public IReportSharpOptionsBuilder AddReporter<TReporter, TReporterOptionsBuilder>(
            Func<TReporterOptionsBuilder> reporterOptionsBuilder)
            where TReporter : IDataReporter, IExceptionReporter, IRequestReporter
            where TReporterOptionsBuilder :
            IDataReporterOptionsBuilder<TReporter>,
            IExceptionReporterOptionsBuilder<TReporter>,
            IRequestReporterOptionsBuilder<TReporter>
        {
            (reporterOptionsBuilder() as IDataReporterOptionsBuilder<TReporter>).Build(ServiceCollection);

            ServiceCollection.AddScoped(typeof(IRequestReporter), typeof(TReporter));
            ServiceCollection.AddScoped(typeof(IExceptionReporter), typeof(TReporter));
            ServiceCollection.AddScoped(typeof(IDataReporter), typeof(TReporter));

            return this;
        }

        public IReportSharpOptionsBuilder AddExceptionReporter<TReporter>(
            Func<IExceptionReporterOptionsBuilder<TReporter>> reporterOptionsBuilder)
            where TReporter : IExceptionReporter
        {
            reporterOptionsBuilder().Build(ServiceCollection);

            ServiceCollection.AddScoped(typeof(IExceptionReporter), typeof(TReporter));

            return this;
        }

        public IReportSharpOptionsBuilder AddRequestReporter<TReporter>(
            Func<IRequestReporterOptionsBuilder<TReporter>> reporterOptionsBuilder) where TReporter : IRequestReporter
        {
            reporterOptionsBuilder().Build(ServiceCollection);

            ServiceCollection.AddScoped(typeof(IRequestReporter), typeof(TReporter));

            return this;
        }

        public IReportSharpOptionsBuilder AddDataReporter<TReporter>(
            Func<IDataReporterOptionsBuilder<TReporter>> reporterOptionsBuilder) where TReporter : IDataReporter
        {
            reporterOptionsBuilder().Build(ServiceCollection);

            ServiceCollection.AddScoped(typeof(IDataReporter), typeof(TReporter));

            return this;
        }

        public IReportSharpOptionsBuilder ConfigureReportSharp(
            Action<IReportSharpConfigOptionsBuilder> reportSharpConfigOptionsBuilder)
        {
            reportSharpConfigOptionsBuilder(new ReportSharpConfigOptionsBuilder(ServiceCollection));

            return this;
        }

        public IReportSharpOptionsBuilder AddApis(Action<IApiOptionsBuilder> options)
        {
            ServiceCollection.AddMvc(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false
            );

            options(new ApiOptionsBuilder.ApiOptionsBuilder(ServiceCollection));

            return this;
        }
    }
}