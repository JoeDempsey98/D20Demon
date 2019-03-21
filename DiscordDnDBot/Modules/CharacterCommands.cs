using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordDnDBot.Modules
{
    class CharacterCommands
    {
        static string path = "SystemLang/characters.json";

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
    }
}
