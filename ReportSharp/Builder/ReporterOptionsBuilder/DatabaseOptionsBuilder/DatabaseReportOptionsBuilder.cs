using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.DbContext;
using ReportSharp.Reporters.DatabaseReporter;

namespace ReportSharp.Builder.ReporterOptionsBuilder.DatabaseOptionsBuilder
{
    public class DatabaseReportOptionsBuilder<TDbContext> :
        IDataReporterOptionsBuilder<DatabaseReporter>,
        IExceptionReporterOptionsBuilder<DatabaseReporter>,
        IRequestReporterOptionsBuilder<DatabaseReporter>
        where TDbContext : ReportSharpDbContext
    {
        private Action<DbContextOptionsBuilder> _optionsBuilder;

        public void Build(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<TDbContext>(_optionsBuilder);
        }

        public DatabaseReportOptionsBuilder<TDbContext> SetOptionsBuilder(
            Action<DbContextOptionsBuilder> optionsBuilder
        )
        {
            _optionsBuilder = optionsBuilder;

            return this;
        }
    }
}