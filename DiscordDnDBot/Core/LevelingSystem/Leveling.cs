using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordDnDBot.Core.LevelingSystem
{
    internal static class Leveling
    {
        internal static async void OnUserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            //if user is timedout, ignore
            var userAccount = UserAccounts.UserAccounts.GetAccount(user);
            TimeSpan difference = DateTime.Now - userAccount.lastSentMessage;
            if (difference.TotalSeconds < 7)
            {
                Console.WriteLine("Messages too close in time to award XP");
                return;
            }
            //set the user's last sent message time to now
            userAccount.lastSentMessage = DateTime.Now;
            //calculate xp and level gains
            uint oldLvl = userAccount.lvl;
            userAccount.XP += 20;
            UserAccounts.UserAccounts.SaveUserAccounts();
            uint newLvl = userAccount.lvl;

            if (oldLvl != userAccount.lvl)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(50, 170, 60);
                embed.WithTitle("DING!");
                embed.WithDescription(user.Username + " has leveled up!");
                embed.AddField("Level", newLvl, true);
                embed.AddField("XP", userAccount.XP, true);

                await channel.SendMessageAsync("", embed: embed.Build());
            }
        }
    }
}
