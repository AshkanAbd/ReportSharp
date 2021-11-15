using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReportSharp.Models;

namespace ReportSharp.Reporters
{
    public interface IExceptionReporter
    {
        public Task ReportException(HttpContext httpContext, ReportSharpRequest request, Exception exception);
    }
}