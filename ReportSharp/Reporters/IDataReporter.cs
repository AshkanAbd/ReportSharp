using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ReportSharp.Reporters
{
    public interface IDataReporter
    {
        public Task ReportData(HttpContext httpContext, string tag, string data);
    }
}