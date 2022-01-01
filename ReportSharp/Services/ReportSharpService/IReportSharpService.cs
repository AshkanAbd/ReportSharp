using ReportSharp.Models;

namespace ReportSharp.Services.ReportSharpService
{
    public interface IReportSharpService
    {
        public ReportSharpRequest GetRequest();
        internal void SetRequest(ReportSharpRequest reportSharpRequest);
    }
}