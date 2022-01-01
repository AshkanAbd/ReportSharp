using System;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Configs;

namespace ReportSharp.Builder.ReportSharpConfigBuilder
{
    public class ReportSharpConfigBuilder : IReportSharpConfigBuilder
    {
        public ReportSharpConfigBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; }

        public IReportSharpConfigBuilder SetWatchdogPrefix(string watchdogPrefix)
        {
            if (watchdogPrefix == null) throw new ArgumentNullException(nameof(watchdogPrefix));

            ServiceCollection.Configure<ReportSharpConfig>(config =>
                config.WatchdogPrefix = watchdogPrefix
            );

            return this;
        }
    }
}