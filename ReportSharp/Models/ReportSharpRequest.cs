using System;
using System.Text;
using SoftDeletes.ModelTools;

namespace ReportSharp.Models
{
    public class ReportSharpRequest : ITimestamps
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Verb { get; set; }
        public string Headers { get; set; }
        public string QueryParameters { get; set; }
        public string Body { get; set; }
        public string Ip { get; set; }

        public ReportSharpResponse ReportSharpResponse { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Request: {").Append('\n')
                .Append("Id: ").Append(Id).Append('\n')
                .Append("Url: ").Append(Url).Append('\n')
                .Append("Verb: ").Append(Verb).Append('\n')
                .Append("Headers: ").Append(Headers).Append('\n')
                .Append("Queries: ").Append(QueryParameters).Append('\n')
                .Append("Body: ").Append(Body).Append('\n')
                .Append("IP: ").Append(Ip).Append('\n')
                .Append('}').ToString();
        }
    }
}