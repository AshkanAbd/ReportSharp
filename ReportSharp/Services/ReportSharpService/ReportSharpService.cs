using ReportSharp.Models;

namespace ReportSharp.Services.ReportSharpService
{
    internal class ReportSharpService : IReportSharpService
    {
        private ReportSharpRequest _reportSharpRequest;

        void IReportSharpService.SetRequest(ReportSharpRequest reportSharpRequest)
        {
            _reportSharpRequest = reportSharpRequest;
        }

        public ReportSharpRequest GetRequest()
        {
            return _reportSharpRequest;
        }
    }
}