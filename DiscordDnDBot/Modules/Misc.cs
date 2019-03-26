using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Rest;
using DiscordDnDBot.Core.UserAccounts;

namespace DiscordDnDBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("lvl?")]
        public async Task CalculateLevel()
        {
            var userAccount = UserAccounts.GetAccount(Context.User);
            await Context.Channel.SendMessageAsync(Context.User.Username + " is level " + userAccount.lvl);
        }

        [Command("react")]
        public async Task HandleReactionMessage()
        {
            RestUserMessage msg = await Context.Channel.SendMessageAsync("Test Message, react to test.");
            Global.MessageIdTracker = msg.Id;
        }

        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));
            
            await Context.Channel.SendMessageAsync("",false, embed.Build());
        }

        [Command("pick")]
        public async Task Pick([Remainder]string message)
        {
            string[] options = message.Split('|', StringSplitOptions.RemoveEmptyEntries);

            Random rand = new Random();
            string selection = options[rand.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(65, 255, 100));
            embed.WithThumbnailUrl("https://media1.tenor.com/images/5c406b927ec59a31eb67e3366f3121ef/tenor.gif?itemid=11909469");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("users")]
        public async Task GetUsers()
        {
            EmbedBuilder embed = new EmbedBuilder();

            foreach(SocketGuildUser user in Context.Guild.Users)
            {
                SocketUser current = user;
                embed.WithDescription(current.Username);
            }

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("create")]
        public async Task CreateCharacter([Remainder]string name)
        {
            CharacterSheet character = new CharacterSheet(name, Context.User.Username);
            CharacterCommands.UpdateCharactersJson(character);
            await Context.Channel.SendMessageAsync("Creating " + name);
        }

        [Command("show")]
        public async Task ShowCharacter([Remainder]string name)
        {
            EmbedBuilder embed = CharacterCommands.DisplayCharacter(name, Context);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("roll")]
        public async Task RollDice([Remainder]string str)
        {
            string[] diceCode = str.Split('d');
            if (diceCode.Length < 2)
            {
                await Context.Channel.SendMessageAsync(Utilities.GetAlert("DICE_ROLL_FAIL"));
                return;
            }
            Dice dice = new Dice(Int32.Parse(diceCode[1]));
            int roll = dice.Roll(Int32.Parse(diceCode[0]));

            await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("DICE_ROLL_RESULT", roll));
        }

        [Command("str")]
        public async Task RollStrength()
        {
            EmbedBuilder embed = CharacterCommands.RollStr((SocketGuildUser)Context.User);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("dex")]
        public async Task RollDexterity()
        {
            EmbedBuilder embed = CharacterCommands.RollDex((SocketGuildUser)Context.User);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("con")]
        public async Task RollConstitution()
        {
            EmbedBuilder embed = CharacterCommands.RollCon((SocketGuildUser)Context.User);
            
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        
        [Command("wis")]
        public async Task RollWisdom()
        {
            EmbedBuilder embed = CharacterCommands.RollWis((SocketGuildUser)Context.User);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("int")]
        public async Task RollIntelligence()
        {
            EmbedBuilder embed = CharacterCommands.RollInt((SocketGuildUser)Context.User);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("cha")]
        public async Task RollCharisma()
        {
            EmbedBuilder embed = CharacterCommands.RollCha((SocketGuildUser)Context.User);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("class")]
        public async Task GetOrSetClass([Remainder]string input = null)
        {
            string charName = null;
            string className = null;
            if (input != null)
            {
                string[] inputFormatted = input.Split(' ');
                charName = inputFormatted[0];
                if (inputFormatted.Length >= 2)
                {
                    for (int i = 1; i < inputFormatted.Length; i++)
                        className += inputFormatted[i];
                }
            }

            EmbedBuilder embed = null;
            if (charName == null && className == null)
                await Context.Channel.SendMessageAsync(Utilities.GetAlert("CLASS_USAGE"));
            else if (charName != null && className == null)
            {
                embed = CharacterCommands.DisplayOrChangeClass((SocketGuildUser)Context.User, charName);
            }
            else if (charName != null && className != null)
            {
                embed = CharacterCommands.DisplayOrChangeClass((SocketGuildUser)Context.User, charName, className);
            }
            
            if (embed == null && (charName != null || className != null))
            {
                embed = new EmbedBuilder();
                embed.WithDescription("Something went wrong, make sure everything was typed correctly");
            }
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("List of Available Commands:");
            embed.AddField("$create X", "Create a new character with the name 'X'");
            embed.AddField("$show X", "Show the character with the name 'X'");
            embed.AddField("$kick X Y", "Kick a user with the name 'X', for the reason 'Y' (by default this is 'reasons')");
            embed.AddField("$ban X Y", "Ban a user with the name 'X', for the reason 'Y' (by default this is 'reasons')");
            embed.WithColor(Color.DarkRed);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("mute")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task MuteUser(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.isMuted = true;
            UserAccounts.SaveUserAccounts();
        }

        [Command("unmute")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task UnmuteUser(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.isMuted = false;
            UserAccounts.SaveUserAccounts();
        }

        [Command("warn")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task WarnUser(IGuildUser user, [Remainder]string reason = "No warning reason provided.")
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumOfWarnings++;
            UserAccounts.SaveUserAccounts();

            SocketGuildUser guildUser = (SocketGuildUser)user;
            await guildUser.SendMessageAsync(reason);
            //check num of warnings
            if(userAccount.NumOfWarnings >= 3)
            {
                userAccount.isMuted = true;
                UserAccounts.SaveUserAccounts();
            }
        }

        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, [Remainder]string reason = "No kick reason provided.")
        {
            await user.KickAsync(reason);
        }
        
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, [Remainder]string reason = "No ban reason provided.")
        {
            await user.BanAsync(reason: reason);
        }
    }
}