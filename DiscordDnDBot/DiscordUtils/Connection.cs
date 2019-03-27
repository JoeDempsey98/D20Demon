using Discord;
using Discord.Net;
using Discord.Net.Providers;
using Discord.WebSocket;
using DiscordDnDBot.Core;
using System.Threading.Tasks;

namespace DiscordDnDBot.DiscordUtils
{
    internal class Connection
    {
        static DiscordSocketClient _client;
        static CommandHandler _handler;

        public static async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                WebSocketProvider = Discord.Net.Providers.WS4Net.WS4NetProvider.Instance
            });
            _client.Log += Logger.Log;
            _client.Ready += RepeatingTimer.StartTimer;
            _client.ReactionAdded += OnReactionAdded;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            Global.client = _client;
            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }
        private static async Task OnReactionAdded(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.MessageId == Global.MessageIdTracker)
            {
                if (reaction.Emote.Name == "👌")
                {
                    Emoji emoji = (Emoji)reaction.Emote;
                    await cache.Value.AddReactionAsync(emoji);
                }
            }
        }
    }
}
