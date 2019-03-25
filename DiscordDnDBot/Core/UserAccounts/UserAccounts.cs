using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Discord.WebSocket;

namespace DiscordDnDBot.Core.UserAccounts
{
    static class UserAccounts
    {
        public static List<UserAccount> userAccounts;
        public static string path = "Core/UserAccounts/UserAccounts.json";

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
            return userAccounts;
        }
        
        public static void AddNewUser(SocketGuildUser guildUser)
        {
            LoadUserAccounts();
            UserAccount user = new UserAccount(guildUser.Id, guildUser.Username);
            if(userAccounts == null)
            {
                userAccounts.Add(user);
                SaveUserAccounts();
            }
            else if (!userAccounts.Contains(user))
            {
                userAccounts.Add(user);
            }
        }
    }
}
