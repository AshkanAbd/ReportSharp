using System;
using System.Text;
using SoftDeletes.ModelTools;

namespace ReportSharp.Models
{
    public class ReportSharpResponse : ITimestamps
    {
        public long Id { get; set; }
        public string StatusCode { get; set; }
        public string Content { get; set; }
        public string Headers { get; set; }
        public long RequestId { get; set; }

        public ReportSharpRequest ReportSharpRequest { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Response: {").Append('\n')
                .Append("Id: ").Append(Id).Append('\n')
                .Append("StatusCode: ").Append(StatusCode).Append('\n')
                .Append("Content: ").Append(Content).Append('\n')
                .Append("Headers: ").Append(Headers).Append('\n')
                .Append('}').ToString();
        }
    }
}