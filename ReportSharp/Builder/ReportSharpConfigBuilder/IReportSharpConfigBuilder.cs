using Microsoft.Extensions.DependencyInjection;

namespace ReportSharp.Builder.ReportSharpConfigBuilder
{
    public interface IReportSharpConfigBuilder
    {
        public IServiceCollection ServiceCollection { get; }
        public IReportSharpConfigBuilder SetWatchdogPrefix(string watchdogPrefix);
    }
}