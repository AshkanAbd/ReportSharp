using ReportSharp.Models;

namespace ReportSharp.Services.ReportSharpService
{
    internal class ReportSharpService : IReportSharpService
    {
        private ReportSharpRequest _reportSharpRequest;

        void IReportSharpService.SetCurrentLog(ReportSharpRequest reportSharpRequest)
        {
            _reportSharpRequest = reportSharpRequest;
        }

        public ReportSharpRequest GetCurrentLog()
        {
            return _reportSharpRequest;
        }
    }
}