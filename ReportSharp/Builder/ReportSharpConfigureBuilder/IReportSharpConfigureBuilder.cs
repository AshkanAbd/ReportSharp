using System;
using ReportSharp.Middlewares;

namespace ReportSharp.Builder.ReportSharpConfigureBuilder
{
    public interface IReportSharpConfigureBuilder
    {
        public IReportSharpConfigureBuilder UseApis();
        public IReportSharpConfigureBuilder UseReportSharpMiddleware<T>() where T : IReportSharpMiddleware;
        public IReportSharpConfigureBuilder UseReportSharpMiddleware(Type type);
    }
}