using Discord;
using System;
using System.Threading.Tasks;

namespace DiscordDnDBot.DiscordUtils
{
    class Logger
    {
        internal static async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
