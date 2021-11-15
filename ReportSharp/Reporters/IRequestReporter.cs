using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ReportSharp.Models;

namespace ReportSharp.Reporters
{
    public interface IRequestReporter
    {
        public Task ReportRequest(HttpContext httpContext, ReportSharpRequest request);
    }
}