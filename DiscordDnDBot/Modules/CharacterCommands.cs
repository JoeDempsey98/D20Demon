using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordDnDBot.Modules
{
    class CharacterCommands
    {
        static string path = "Resources/Characters/characters.json";

        public static void UpdateCharactersJson(CharacterSheet character)
        {
            string jsonData = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(jsonData)
                ?? new List<CharacterSheet>();

            CharacterSheet temp = null;
            foreach(CharacterSheet sheet in characterSheets)
            {
                if (character.characterName == sheet.characterName)
                    temp = sheet;
            }
            characterSheets.Remove(temp);
            characterSheets.Add(character);
            
            jsonData = JsonConvert.SerializeObject(characterSheets, Formatting.Indented);
            File.WriteAllText(path, jsonData);
        }

        public static EmbedBuilder DisplayCharacter(string name, SocketCommandContext context)
        {
            EmbedBuilder embed = new EmbedBuilder();
            string jsonData = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(jsonData);

            embed.WithAuthor(context.User);
            embed.WithColor(Color.DarkOrange);
            

            foreach (CharacterSheet character in characterSheets)
            {
                if (character.characterName.ToLower() == name.ToLower())
                {
                    character.GetStats();
                    character.SetMaxHp(10);
                    
                    UpdateCharactersJson(character);
                    embed.WithDescription(character.characterName);
                    embed.AddField("Strength", character.strength, true);
                    embed.AddField("Dexterity", character.dexterity, true);
                    embed.AddField("Constitution", character.constitution, true);
                    embed.AddField("Wisdom", character.wisdom, true);
                    embed.AddField("Intelligence", character.intelligence, true);
                    embed.AddField("Charisma", character.charisma, true);
                    UpdateCharactersJson(character);


                    if (character.characterName == "Rickey Smiley")
                        embed.WithImageUrl("http://www.gstatic.com/tv/thumb/persons/264690/264690_v9_bb.jpg");
                }
            }
            return embed;
        }
        public static EmbedBuilder RollStr(SocketGuildUser user)
        {
            Dice d20 = new Dice(20);
            EmbedBuilder embed = new EmbedBuilder();
            string json = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(json);
            embed.WithAuthor(user);

            foreach (CharacterSheet character in characterSheets)
            {
                if (character.playerName == user.Username)
                    embed.AddField(character.characterName, Utilities.GetFormattedAlert("ABILITY_ROLL_RESULT", "Strength", character.GetMod(character.strength), character.characterName, d20.Roll() + character.GetMod(character.strength)));
            }

            return embed;
        }
        public static EmbedBuilder RollDex(SocketGuildUser user)
        {
            Dice d20 = new Dice(20);
            EmbedBuilder embed = new EmbedBuilder();
            string json = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(json);
            embed.WithAuthor(user);

            foreach (CharacterSheet character in characterSheets)
            {
                if (character.playerName == user.Username)
                    embed.AddField(character.characterName, Utilities.GetFormattedAlert("ABILITY_ROLL_RESULT", "Dexterity", character.GetMod(character.dexterity), character.characterName, d20.Roll() + character.GetMod(character.dexterity)));
            }

            return embed;
        }
        public static EmbedBuilder RollCon(SocketGuildUser user)
        {
            Dice d20 = new Dice(20);
            EmbedBuilder embed = new EmbedBuilder();
            string json = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(json);
            embed.WithAuthor(user);

            foreach (CharacterSheet character in characterSheets)
            {
                if (character.playerName == user.Username)
                    embed.AddField(character.characterName, Utilities.GetFormattedAlert("ABILITY_ROLL_RESULT", "Constitution", character.GetMod(character.constitution), character.characterName, d20.Roll() + character.GetMod(character.constitution)));
            }

            return embed;
        }
        public static EmbedBuilder RollWis(SocketGuildUser user)
        {
            Dice d20 = new Dice(20);
            EmbedBuilder embed = new EmbedBuilder();
            string json = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(json);
            embed.WithAuthor(user);

            foreach (CharacterSheet character in characterSheets)
            {
                if (character.playerName == user.Username)
                    embed.AddField(character.characterName, Utilities.GetFormattedAlert("ABILITY_ROLL_RESULT", "Wisdom", character.GetMod(character.wisdom), character.characterName, d20.Roll() + character.GetMod(character.wisdom)));
            }

            return embed;
        }
        public static EmbedBuilder RollInt(SocketGuildUser user)
        {
            Dice d20 = new Dice(20);
            EmbedBuilder embed = new EmbedBuilder();
            string json = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(json);
            embed.WithAuthor(user);

            foreach (CharacterSheet character in characterSheets)
            {
                if (character.playerName == user.Username)
                    embed.AddField(character.characterName, Utilities.GetFormattedAlert("ABILITY_ROLL_RESULT", "Intelligence", character.GetMod(character.intelligence), character.characterName, d20.Roll() + character.GetMod(character.intelligence)));
            }

            return embed;
        }
        public static EmbedBuilder RollCha(SocketGuildUser user)
        {
            Dice d20 = new Dice(20);
            EmbedBuilder embed = new EmbedBuilder();
            string json = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(json);
            embed.WithAuthor(user);

            foreach (CharacterSheet character in characterSheets)
            {
                if (character.playerName == user.Username)
                    embed.AddField(character.characterName, Utilities.GetFormattedAlert("ABILITY_ROLL_RESULT", "Charisma", character.GetMod(character.charisma), character.characterName, d20.Roll() + character.GetMod(character.charisma)));
            }

            return embed;
        }
        public static EmbedBuilder DisplayOrChangeClass(SocketGuildUser user, string charName, string charClass = null)
        {
            Dice d20 = new Dice(20);
            EmbedBuilder embed = new EmbedBuilder();
            string json = File.ReadAllText(path);
            List<CharacterSheet> characterSheets = JsonConvert.DeserializeObject<List<CharacterSheet>>(json);
            bool found = false;
            
            foreach (CharacterSheet character in characterSheets)
            {
                if (character.characterName == charName)
                {
                    if (character.playerName == user.Username && charClass != null)
                    {
                        character.characterClass = charClass;
                    }
                    embed.WithAuthor(character.playerName);
                    embed.AddField(character.characterName, Utilities.GetFormattedAlert("CLASS_DISPLAY", character.characterName, character.characterClass));
                    found = true;
                }
            }
            json = JsonConvert.SerializeObject(characterSheets);
            File.WriteAllText(path, json);

            if (!found)
                embed.WithDescription(Utilities.GetAlert("CLASS_ERROR"));

            return embed;
        }
    }
}
