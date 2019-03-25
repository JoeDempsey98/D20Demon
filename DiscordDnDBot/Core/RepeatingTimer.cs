﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord.WebSocket;
using DiscordDnDBot.Core.DateAndTime;
using DiscordDnDBot.Core.UserAccounts;

namespace DiscordDnDBot.Core
{
    internal static class RepeatingTimer
    {
        private static Timer loopingTimer;
        private static SocketTextChannel channel;
        private static string time;

        internal static Task StartTimer()
        {
            loopingTimer = new Timer()
            {
                Interval = 5000,
                AutoReset = true,
                Enabled = true
            };

            channel = Global.client.GetGuild(557750316330647552).GetTextChannel(557750316804866079);

            loopingTimer.Elapsed += OnTimerTickComplete;

            return Task.CompletedTask;
        }

        private static async void OnTimerTickComplete(object sender, ElapsedEventArgs e)
        {
            if (Global.client == null)
            {
                Console.WriteLine("Timer ticked, but client wasn't saved globally.");
                return;
            }

            if (DateTime.Now.ToShortDateString() != DateAndTimeManagement.LoadDate())
            {
                DateAndTimeManagement.SaveDate();
                await channel.SendMessageAsync("Dawn of a New Day");
            }

            IReadOnlyCollection<SocketGuild> guilds = Global.client.Guilds;
            IReadOnlyCollection<SocketUser> users;
            foreach (SocketGuild guild in guilds)
            {
                users = guild.Users;
                foreach (SocketGuildUser user in users)
                {
                    UserAccounts.UserAccounts.AddNewUser(user);
                }
            }

            foreach (UserAccount u in UserAccounts.UserAccounts.userAccounts)
            {
                u.AddXP(1);
                u.AddMoney(1);
            }
            UserAccounts.UserAccounts.SaveUserAccounts();
        }
    }
}
