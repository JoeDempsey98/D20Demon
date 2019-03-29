using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordDnDBot.Resources.Request
{
    public class Races
    {
        public class StartingProficiency
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class From
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class StartingProficiencyOptions
        {
            public int choose { get; set; }
            public string type { get; set; }
            public List<From> from { get; set; }
        }

        public class Language
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Trait
        {
            public string url { get; set; }
            public string name { get; set; }
        }

        public class Subrace
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class RootObject
        {
            public string _id { get; set; }
            public int index { get; set; }
            public string name { get; set; }
            public int speed { get; set; }
            public int[] ability_bonuses { get; set; }
            public string alignment { get; set; }
            public string age { get; set; }
            public string size { get; set; }
            public string size_description { get; set; }
            public List<StartingProficiency> starting_proficiencies { get; set; }
            public StartingProficiencyOptions starting_proficiency_options { get; set; }
            public List<Language> languages { get; set; }
            public string language_desc { get; set; }
            public List<Trait> traits { get; set; }
            public List<Subrace> subraces { get; set; }
            public string url { get; set; }
        }
    }
}
