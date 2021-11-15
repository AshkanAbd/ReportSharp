using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.Extensions.Options;

namespace ReportSharp.Services.DiscordService
{
    public class DiscordService : IDiscordService
    {
        private readonly HashSet<DiscordChannel> _discordChannels = new HashSet<DiscordChannel>();
        private readonly DiscordClient _discordClient;

        public DiscordService(IOptions<DiscordConfig> options)
        {
            _discordClient = new DiscordClient(new DiscordConfiguration {
                Token = options.Value.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            new TaskFactory<Task>()
                .StartNew(async () => await Connect())
                .Unwrap()
                .GetAwaiter()
                .GetResult();

            AddChannels(options.Value.Channels);
        }

        public DiscordClient GetDiscordClient()
        {
            return _discordClient;
        }

        public HashSet<DiscordChannel> GetChannels()
        {
            return _discordChannels;
        }

        public async Task SendMessage(string content)
        {
            foreach (var discordChannel in _discordChannels)
                for (var i = 0; i < content.Length; i += 1999)
                    await _discordClient.SendMessageAsync(discordChannel, content.Substring(i,
                        i + 1999 > content.Length ? content.Length - i : 1999
                    ));
        }

        private async Task Connect()
        {
            await _discordClient.ConnectAsync();
        }

        private void AddChannels(IEnumerable<ulong> channelIds)
        {
            foreach (var channelId in channelIds)
                try {
                    var channel = new TaskFactory<Task<DiscordChannel>>()
                        .StartNew(async () => await _discordClient.GetChannelAsync(channelId))
                        .Unwrap()
                        .GetAwaiter()
                        .GetResult();

                    _discordChannels.Add(channel);

                    var message = new TaskFactory<Task<DiscordMessage>>()
                        .StartNew(async () =>
                            await _discordClient.SendMessageAsync(channel, "ReportSharp activated.")
                        ).Unwrap()
                        .GetAwaiter()
                        .GetResult();
                    Console.WriteLine(message);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
        }
    }
}