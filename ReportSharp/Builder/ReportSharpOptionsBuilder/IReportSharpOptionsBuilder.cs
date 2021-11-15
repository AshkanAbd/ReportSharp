using System;
using ReportSharp.Builder.ApiOptionsBuilder;
using ReportSharp.Builder.ConfigOptionsBuilder;
using ReportSharp.Builder.ReporterOptionsBuilder;
using ReportSharp.Reporters;

namespace ReportSharp.Builder.ReportSharpOptionsBuilder
{
    public interface IReportSharpOptionsBuilder
    {
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

        public IReportSharpOptionsBuilder AddApis(Action<IApiOptionsBuilder> options);

        public IReportSharpOptionsBuilder ConfigureReportSharp(
            Action<IReportSharpConfigOptionsBuilder> reportSharpConfigOptionsBuilder);
    }
}