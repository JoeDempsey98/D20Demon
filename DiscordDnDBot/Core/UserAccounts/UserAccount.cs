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
        public uint lvl;

        public UserAccount(ulong id, string username)
        {
            this.id = id;
            this.username = username;
            XP = 0;
            money = 300;
            lvl = 1;
        }

        public void AddXP(uint amount)
        {
            XP += amount;
            CheckLvl();
        }

        public void AddMoney(uint amount)
        {
            money += amount;
        }

        public void CheckLvl()
        {
            if (XP >= 100 * lvl)
                lvl++;
        }
    }
}
