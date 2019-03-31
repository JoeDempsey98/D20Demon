using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordDnDBot.Resources.Request
{
    public class Items
    {
        public class Cost
        {
            public int quantity { get; set; }
            public string unit { get; set; }
        }

        public class DamageType
        {
            public string url { get; set; }
            public string name { get; set; }
        }

        public class Damage
        {
            public int dice_count { get; set; }
            public int dice_value { get; set; }
            public DamageType damage_type { get; set; }
        }

        public class Range
        {
            public int normal { get; set; }
            public string @long { get; set; }
        }

        public class Property
        {
            public string name { get; set; }
            public string url { get; set; }
        }
        public class ArmorClass
        {
            public int @base { get; set; }
            public bool dex_bonus { get; set; }
            public int max_bonus { get; set; }
        }
        public class RootObject
        {
            public string _id { get; set; }
            public int index { get; set; }
            public string name { get; set; }
            public string equipment_category { get; set; }
            public string category_range { get; set; }
            public Cost cost { get; set; }
            public Damage damage { get; set; }
            public Range range { get; set; }
            public double weight { get; set; }
            public List<Property> properties { get; set; }
            public string url { get; set; }
        }
    }
}
