using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordDnDBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));
            
            await Context.Channel.SendMessageAsync("",false, embed.Build());
        }

        [Command("pick")]
        public async Task Pick([Remainder]string message)
        {
            string[] options = message.Split('|', StringSplitOptions.RemoveEmptyEntries);

            Random rand = new Random();
            string selection = options[rand.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(65, 255, 100));
            embed.WithThumbnailUrl("https://media1.tenor.com/images/5c406b927ec59a31eb67e3366f3121ef/tenor.gif?itemid=11909469");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("users")]
        public async Task GetUsers()
        {
            EmbedBuilder embed = new EmbedBuilder();

            foreach(SocketGuildUser user in Context.Guild.Users)
            {
                SocketUser current = user;
                embed.WithDescription(current.Username);
            }

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("kick")]
        public async Task Kick([Remainder]string str)
        {
            string[] userAndReason = str.Split(" | ");
            string offender = userAndReason[0];
            string reason;
            if (userAndReason.Length < 2)
                reason = "reasons";
            else
                reason = userAndReason[1];

            foreach (SocketGuildUser user in Context.Guild.Users)
            {                
                SocketUser current = user;
                if (current.Username == userAndReason[0])
                {
                    await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("KICK_&NAME_&REASON", offender, reason));
                    await user.KickAsync(reason);
                }
            }
        }

        [Command("ban")]
        public async Task Ban([Remainder]string str)
        {
            string[] userAndReason = str.Split(" | ");
            string offender = userAndReason[0];
            string reason;
            if (userAndReason.Length < 2)
                reason = "reasons";
            else
                reason = userAndReason[1];

            foreach (SocketGuildUser user in Context.Guild.Users)
            {
                SocketUser current = user;
                if (current.Username == userAndReason[0])
                {
                    await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("BAN_&NAME_&REASON", offender, reason));
                    await user.KickAsync(reason);
                }
            }
        }
    }
}