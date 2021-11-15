using ReportSharp.Models;

namespace ReportSharp.Services.ReportSharpService
{
    public interface IReportSharpService
    {
        public ReportSharpRequest GetCurrentLog();
        internal void SetCurrentLog(ReportSharpRequest reportSharpRequest);
    }
}