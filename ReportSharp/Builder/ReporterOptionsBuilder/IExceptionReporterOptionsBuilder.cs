using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Reporters;

namespace ReportSharp.Builder.ReporterOptionsBuilder
{
    public interface IExceptionReporterOptionsBuilder<TExceptionReporter> where TExceptionReporter : IExceptionReporter
    {
        public void Build(IServiceCollection serviceCollection);
    }
}