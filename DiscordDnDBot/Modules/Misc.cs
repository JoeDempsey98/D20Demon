using Discord.Commands;
using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Rest;
using DiscordDnDBot.Core.UserAccounts;
using DiscordDnDBot.Core.LevelingSystem;
using System.IO;
using DiscordDnDBot.Resources.Request;

namespace DiscordDnDBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("items")]
        public async Task FetchItemInfo([Remainder]string name)
        {
            var embed = _5eApi.FetchItemInfo(name);
            Embed embed1 = embed.Build();

            await Context.Channel.SendMessageAsync(embed: embed.Build());
            if (embed1.Length >= 25) await Context.Channel.SendMessageAsync("Search yielded to many results to display, consider refining.");
        }

        [Command("races")]
        public async Task FetchRacesInfo([Remainder]string name)
        {
            var embed = _5eApi.FetchRaceInfo(name);
            embed.WithColor(Color.DarkGreen);
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("classes")]
        public async Task FetchClassesInfo([Remainder]string name)
        {
            var embed = _5eApi.FetchClassInfo(name);
            embed.WithColor(Color.Blue);
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("spells")]
        public async Task FetchSpellsInfo([Remainder]string name)
        {
            var embed = _5eApi.FetchSpellInfo(name);
            embed.WithColor(Color.DarkMagenta);
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("card")]
        public async Task DisplayUserCard()
        {
            var user = UserAccounts.GetAccount(Context.User);
            await Context.Channel.SendFileAsync(new MemoryStream(UserLevelDisplayImage.HtmlToJpeg(user)),"testImage.jpg");
        }

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
            character.RollStats();
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

        [Command("ability")]
        public async Task RollAbility([Remainder]string ability)
        {
            EmbedBuilder embed;

            switch (ability)
            {
                case "str":
                    embed = CharacterCommands.RollStr((SocketGuildUser)Context.User);
                    break;
                case "dex":
                    embed = CharacterCommands.RollDex((SocketGuildUser)Context.User);
                    break;
                case "con":
                    embed = CharacterCommands.RollCon((SocketGuildUser)Context.User);
                    break;
                case "wis":
                    embed = CharacterCommands.RollWis((SocketGuildUser)Context.User);
                    break;
                case "int":
                    embed = CharacterCommands.RollInt((SocketGuildUser)Context.User);
                    break;
                case "cha":
                    embed = CharacterCommands.RollCha((SocketGuildUser)Context.User);
                    break;
                case "strength":
                    embed = CharacterCommands.RollStr((SocketGuildUser)Context.User);
                    break;
                case "dexterity":
                    embed = CharacterCommands.RollDex((SocketGuildUser)Context.User);
                    break;
                case "constitution":
                    embed = CharacterCommands.RollCon((SocketGuildUser)Context.User);
                    break;
                case "wisdom":
                    embed = CharacterCommands.RollWis((SocketGuildUser)Context.User);
                    break;
                case "intelligence":
                    embed = CharacterCommands.RollInt((SocketGuildUser)Context.User);
                    break;
                case "charisma":
                    embed = CharacterCommands.RollCha((SocketGuildUser)Context.User);
                    break;
                default:
                    embed = new EmbedBuilder();
                    embed.WithDescription("Unrecognized command, try again");
                    break;
            }
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        
        [Command("class")]
        public async Task GetOrSetClass([Remainder]string className = null)
        {
            string charName = CharacterCommands.GetCharacter((IGuildUser)Context.User).characterName;

            EmbedBuilder embed;
            if (className == null)
            {
                embed = CharacterCommands.DisplayOrChangeClass((SocketGuildUser)Context.User, charName);
            }
            else
            {
                embed = CharacterCommands.DisplayOrChangeClass((SocketGuildUser)Context.User, charName, className);
            }
            
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("List of Available Commands:");
            embed.AddField("$create X", "Create a new character with the name 'X'", true);
            embed.AddField("$show X", "Show the character with the name 'X'", true);
            embed.AddField("$ability X", "Roll various ability checks (strength/str, dexterity/dex, etc.)", true);
            embed.AddField("$class X", "Display or change the class of your character", true);
            embed.AddField("$classes X", "Search for info on class 'X'", true);
            embed.AddField("$spells X", "Search for info on spell 'X'", true);
            embed.AddField("$card", "Display user card", true);
            embed.AddField("$lvl?", "Display user level", true);
            embed.AddField("$mute X", "Mute 'X' user from all text channels", true);
            embed.AddField("$unmute X", "Unmute 'X' user from all text channels", true);
            embed.AddField("$warn X (Y)", "Warn 'X' user for 'Y' reason (by default this is 'No warning reason provided.')", true);
            embed.AddField("$kick X (Y)", "Kick 'X' user for 'Y' reason (by default this is 'No warning reason provided.')", true);
            embed.AddField("$ban X (Y)", "Ban 'X' user for 'Y' reason (by default this is 'No warning reason provided.')", true);
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