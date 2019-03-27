using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using DiscordDnDBot.Core.LevelingSystem;
using DiscordDnDBot.Core.UserAccounts;

namespace DiscordDnDBot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        static CommandService _service;
        
        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);

            //check for user is muted
            var userAccount = UserAccounts.GetAccount(context.User);
            if (userAccount.isMuted)
            {
                await context.Message.DeleteAsync();
                return;
            }

            //Award XP for messages
            if (!context.User.IsBot)
            {
                Leveling.OnUserSentMessage((SocketGuildUser)context.User, (SocketTextChannel)context.Channel);
            }

            int argPos = 0;
            if (msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos) 
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos, services: null);
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
        public static CommandService GetCommandService()
        {
            
            return _service;
        }
    }
}
