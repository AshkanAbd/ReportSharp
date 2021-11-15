using System;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Configs;

namespace ReportSharp.Builder.ConfigOptionsBuilder
{
    public class ReportSharpConfigOptionsBuilder : IReportSharpConfigOptionsBuilder
    {
        public ReportSharpConfigOptionsBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; }

        public IReportSharpConfigOptionsBuilder SetApiPrefix(string apiPrefix)
        {
            if (apiPrefix == null) throw new ArgumentNullException(nameof(apiPrefix));

            ServiceCollection.Configure<ReportSharpConfig>(config =>
                config.ApiPrefix = apiPrefix
            );

            return this;
        }

        public IReportSharpConfigOptionsBuilder SetWatchdogPrefix(string watchdogPrefix)
        {
            if (watchdogPrefix == null) throw new ArgumentNullException(nameof(watchdogPrefix));

            ServiceCollection.Configure<ReportSharpConfig>(config =>
                config.WatchdogPrefix = watchdogPrefix
            );

            return this;
        }

        public IReportSharpConfigOptionsBuilder SetUsername(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            ServiceCollection.Configure<ReportSharpConfig>(config =>
                config.Username = username
            );

            return this;
        }

        public IReportSharpConfigOptionsBuilder SetSecretKey(string secretKey)
        {
            if (secretKey == null) throw new ArgumentNullException(nameof(secretKey));

            ServiceCollection.Configure<ReportSharpConfig>(config =>
                config.SecretKey = secretKey
            );

            return this;
        }
    }
}