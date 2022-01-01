using System;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Builder.ReporterOptionsBuilder;
using ReportSharp.Builder.ReportSharpConfigBuilder;
using ReportSharp.Reporters;

namespace ReportSharp.Builder.ReportSharpOptionsBuilder
{
    public interface IReportSharpOptionsBuilder
    {
        public IServiceCollection ServiceCollection { get; }

        public IReportSharpOptionsBuilder AddReporter<TReporter, TReporterOptionsBuilder>(
            Func<TReporterOptionsBuilder> reporterOptionsBuilder)
            where TReporter : IDataReporter, IExceptionReporter, IRequestReporter
            where TReporterOptionsBuilder :
            IDataReporterOptionsBuilder<TReporter>,
            IExceptionReporterOptionsBuilder<TReporter>,
            IRequestReporterOptionsBuilder<TReporter>;

        public IReportSharpOptionsBuilder AddExceptionReporter<TReporter>(
            Func<IExceptionReporterOptionsBuilder<TReporter>> reporterOptionsBuilder)
            where TReporter : IExceptionReporter;

        public IReportSharpOptionsBuilder AddRequestReporter<TReporter>(
            Func<IRequestReporterOptionsBuilder<TReporter>> reporterOptionsBuilder)
            where TReporter : IRequestReporter;

        public IReportSharpOptionsBuilder AddDataReporter<TReporter>(
            Func<IDataReporterOptionsBuilder<TReporter>> reporterOptionsBuilder)
            where TReporter : IDataReporter;

        public IReportSharpOptionsBuilder ConfigReportSharp(
            Action<IReportSharpConfigBuilder> reportSharpConfigOptionsBuilder);
    }
}