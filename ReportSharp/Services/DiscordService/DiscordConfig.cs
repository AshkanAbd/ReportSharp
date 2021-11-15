using System.Collections.Generic;

namespace ReportSharp.Services.DiscordService
{
    public class DiscordConfig
    {
        public string Token { get; set; }
        public HashSet<ulong> Channels { get; set; }
    }
}