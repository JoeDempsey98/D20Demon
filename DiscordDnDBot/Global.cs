using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordDnDBot
{
    internal static class Global
    {
        internal static DiscordSocketClient client { get; set; }
        internal static ulong MessageIdTracker { get; set; }
    }
}
