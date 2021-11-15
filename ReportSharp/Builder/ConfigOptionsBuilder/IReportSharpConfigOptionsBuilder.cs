namespace ReportSharp.Builder.ConfigOptionsBuilder
{
    public interface IReportSharpConfigOptionsBuilder
    {
        public IReportSharpConfigOptionsBuilder SetApiPrefix(string apiPrefix);
        public IReportSharpConfigOptionsBuilder SetWatchdogPrefix(string watchdogPrefix);
        public IReportSharpConfigOptionsBuilder SetUsername(string username);
        public IReportSharpConfigOptionsBuilder SetSecretKey(string secretKey);
    }
}