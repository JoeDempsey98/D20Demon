using Discord;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static DiscordDnDBot.Resources.Request.Classes;

namespace DiscordDnDBot.Resources.Request
{
    internal static class _5eApi
    {
        static string baseUrl = "http://dnd5eapi.co/api/";

        internal static EmbedBuilder FetchSpellInfo(string name)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Search Results:");
            string json;
            string spells = baseUrl + "spells/";
            using (WebClient webClient = new WebClient())
            {
                json = webClient.DownloadString(spells);
            }
            RootObject request = JsonConvert.DeserializeObject<RootObject>(json);

            var result = from r in request.results
                         where r.name.ToLower().Contains(name.ToLower())
                         select r.url;
            foreach (string url in result)
            {
                using (WebClient webClient = new WebClient())
                {
                    json = webClient.DownloadString(url);
                    Spells.RootObject spellsRootObject = JsonConvert.DeserializeObject<Spells.RootObject>(json);
                    embed.AddField(spellsRootObject.name, spellsRootObject.desc.FirstOrDefault(), true);
                }
            }
            return embed;
        }
        internal static EmbedBuilder FetchClassInfo(string name)
        {
            EmbedBuilder embed = new EmbedBuilder();
            string json;
            string classes = baseUrl + "classes/";
            using (WebClient webClient = new WebClient())
            {
                json = webClient.DownloadString(classes);
            }
            RootObject request = JsonConvert.DeserializeObject<RootObject>(json);

            var result = from r in request.results
                         where r.name.ToLower().Contains(name.ToLower())
                         select r.url;
            foreach (string url in result)
            {
                using (WebClient webClient = new WebClient())
                {
                    json = webClient.DownloadString(url);
                    Classes.RootObject classesRootObject = JsonConvert.DeserializeObject<Classes.RootObject>(json);
                    embed.WithAuthor(classesRootObject.name);
                    embed.AddField("Hit Die", classesRootObject.hit_die);
                    //skill proficiencies
                    string skillProficiencies = string.Format("Pick {0} from", classesRootObject.proficiency_choices.FirstOrDefault().choose);
                    embed.AddField("Skill Proficiencies", skillProficiencies);
                    foreach (From f in classesRootObject.proficiency_choices.FirstOrDefault().from)
                        embed.AddField("§", f.name, true);
                    //saving throws
                    embed.AddField("Saving Throws", "-------");
                    foreach (SavingThrow s in classesRootObject.saving_throws)
                        embed.AddField("§", s.name, true);
                }
            }
            return embed;
        }
        internal static EmbedBuilder FetchRaceInfo(string name)
        {
            EmbedBuilder embed = new EmbedBuilder();
            string json;
            string races = baseUrl + "races/";
            using (WebClient webClient = new WebClient())
            {
                json = webClient.DownloadString(races);
            }
            RootObject request = JsonConvert.DeserializeObject<RootObject>(json);

            var result = from r in request.results
                         where r.name.ToLower().Contains(name.ToLower())
                         select r.url;
            foreach (string url in result)
            {
                using (WebClient webClient = new WebClient())
                {
                    json = webClient.DownloadString(url);
                    Races.RootObject racesRootObject = JsonConvert.DeserializeObject<Races.RootObject>(json);
                    //name
                    embed.AddField(racesRootObject.name, "-------");
                    //speed
                    embed.AddField("Speed", racesRootObject.speed, true);
                    //abilities
                    embed.AddField("Ability Bonuses", "-------");
                    string[] abilities = {"Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma"};
                    for (int i = 0; i < racesRootObject.ability_bonuses.Length; i++)
                    {
                        embed.AddField(abilities[i], racesRootObject.ability_bonuses[i], true);
                    }
                    //proficiencies
                    embed.AddField("Starting Proficiencies", "-------");
                    foreach(Races.StartingProficiency s in racesRootObject.starting_proficiencies)
                    {
                        embed.AddField("§", s.name, true);
                    }
                    //racial traits
                    embed.AddField("Racial Traits", "-------");
                    foreach (Races.Trait t in racesRootObject.traits)
                    {
                        embed.AddField("§", t.name, true);
                    }
                    //subrace
                    embed.AddField("Subrace", racesRootObject.subraces.FirstOrDefault());
                }
            }
            return embed;
        }
        public class Result
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class RootObject
        {
            public int count { get; set; }
            public List<Result> results { get; set; }
        }
    }
}
