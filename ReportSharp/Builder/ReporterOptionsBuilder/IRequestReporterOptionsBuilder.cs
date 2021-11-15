using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Reporters;

namespace ReportSharp.Builder.ReporterOptionsBuilder
{
    public interface IRequestReporterOptionsBuilder<TRequestReporter> where TRequestReporter : IRequestReporter
    {
        public void Build(IServiceCollection serviceCollection);
    }
}