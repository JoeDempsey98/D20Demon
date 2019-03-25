using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Discord.WebSocket;

namespace DiscordDnDBot.Core.UserAccounts
{
    internal static class UserAccounts
    {
        internal static List<UserAccount> userAccounts;
        private static string path = "Core/UserAccounts/UserAccounts.json";

        static UserAccounts()
        {
            userAccounts = new List<UserAccount>();
        }
        public static void SaveUserAccounts()
        {
            if (!File.Exists(path)) File.Create(path);
            string json = JsonConvert.SerializeObject(userAccounts, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        public static List<UserAccount> LoadUserAccounts()
        {
            if (!File.Exists(path)) File.Create(path);
            string json = File.ReadAllText(path);
            userAccounts = JsonConvert.DeserializeObject<List<UserAccount>>(json);
            if (userAccounts == null) userAccounts = new List<UserAccount>();
            return userAccounts;
        }
        
        public static void AddNewUser(SocketUser guildUser)
        {
            LoadUserAccounts();
            UserAccount user = new UserAccount(guildUser.Id, guildUser.Username);
            
            foreach (UserAccount u in userAccounts)
            {
                if (user.id == u.id) return;
            }
            userAccounts.Add(user);
            SaveUserAccounts();
        }

        public static UserAccount GetAccount(SocketUser user)
        {
            LoadUserAccounts();
            foreach (UserAccount u in userAccounts)
            {
                if (u.id == user.Id)
                {
                    return u;
                }
            }
            return null;
        }
    }
}
