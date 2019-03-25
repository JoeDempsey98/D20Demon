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
        public uint XP { get; set; }
        public uint lvl
        {
            get
            {
                //XP = lvl ^ 2 * 50
                return (uint)Math.Sqrt(XP / 50);
            }
        }
        public DateTime lastSentMessage { get; set; } = DateTime.Now;

        public UserAccount(ulong id, string username)
        {
            this.id = id;
            this.username = username;
            XP = 0;
            money = 300;
        }
        
        public void AddMoney(uint amount)
        {
            money += amount;
        }

        public bool SpendMoney (uint amount)
        {
            if (amount > money) return false;
            money -= amount;
            return true;
        }
    }
}
