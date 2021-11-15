using System;
using System.Text;
using SoftDeletes.ModelTools;

namespace ReportSharp.Models
{
    public class ReportSharpData : ITimestamps
    {
        public long Id { get; set; }
        public string Tag { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Data: {").Append('\n')
                .Append("Id: ").Append(Id).Append('\n')
                .Append("Tag: ").Append(Tag).Append('\n')
                .Append("Data: ").Append(Data).Append('\n')
                .Append('}').Append('\n')
                .ToString();
        }
    }
}