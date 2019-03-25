using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DiscordDnDBot.Core.UserAccounts
{
    class UserAccount
    {
        public ulong id;
        public string username;
        public uint money;
        public uint XP;

        public UserAccount(ulong id, string username)
        {
            this.id = id;
            this.username = username;
            XP = 0;
            money = 300;
        }
    }
}
