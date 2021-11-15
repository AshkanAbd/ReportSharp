using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Reporters;

namespace ReportSharp.Builder.ReporterOptionsBuilder
{
    public interface IDataReporterOptionsBuilder<TDataReporter> where TDataReporter : IDataReporter
    {
        public void Build(IServiceCollection serviceCollection);
    }
}