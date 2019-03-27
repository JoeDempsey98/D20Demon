using DiscordDnDBot.DiscordUtils;
using System.Threading.Tasks;

namespace DiscordDnDBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Connection.StartAsync();
        }
    }
}
