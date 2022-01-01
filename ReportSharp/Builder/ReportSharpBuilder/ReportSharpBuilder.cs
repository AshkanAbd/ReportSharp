using System;
using Microsoft.AspNetCore.Builder;
using ReportSharp.Exceptions;
using ReportSharp.Middlewares;

namespace ReportSharp.Builder.ReportSharpBuilder
{
    public class ReportSharpBuilder
    {
        public ReportSharpBuilder(IApplicationBuilder app)
        {
            App = app;
        }

        public IApplicationBuilder App { get; }

        public ReportSharpBuilder UseReportSharpMiddleware<T>() where T : IReportSharpMiddleware
        {
            App.UseMiddleware<T>();

            return this;
        }

        public ReportSharpBuilder UseReportSharpMiddleware(Type type)
        {
            if (!typeof(IReportSharpMiddleware).IsAssignableFrom(type))
                throw new InvalidTypeException(type, "IReportSharp middleware");

            App.UseMiddleware(type);

            return this;
        }
    }
}