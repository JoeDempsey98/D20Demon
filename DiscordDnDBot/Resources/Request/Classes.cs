using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordDnDBot.Resources.Request
{
    public class Classes
    {
        public class From
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class ProficiencyChoice
        {
            public List<From> from { get; set; }
            public string type { get; set; }
            public int choose { get; set; }
        }

        public class Proficiency
        {
            public string url { get; set; }
            public string name { get; set; }
        }

        public class SavingThrow
        {
            public string url { get; set; }
            public string name { get; set; }
        }

        public class StartingEquipment
        {
            public string url { get; set; }
            public string @class { get; set; }
        }

        public class ClassLevels
        {
            public string url { get; set; }
            public string @class { get; set; }
        }

        public class Subclass
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class RootObject
        {
            public string _id { get; set; }
            public int index { get; set; }
            public string name { get; set; }
            public int hit_die { get; set; }
            public List<ProficiencyChoice> proficiency_choices { get; set; }
            public List<Proficiency> proficiencies { get; set; }
            public List<SavingThrow> saving_throws { get; set; }
            public StartingEquipment starting_equipment { get; set; }
            public ClassLevels class_levels { get; set; }
            public List<Subclass> subclasses { get; set; }
            public string url { get; set; }
        }
    }
}
