﻿using DiscordDnDBot.Resources.Characters;
using System;

namespace DiscordDnDBot
{
    class CharacterSheet
    {
        //character details
        public string characterName;
        public string playerName;
        public CharacterClass characterClass { get; set; }
        public string avatarUrl { get; set; }
        //hp
        public int hpMax;
        public int hpCurrent { get; set; }
        //character stats
        public int strength;
        public int dexterity;
        public int constitution;
        public int wisdom;
        public int intelligence;
        public int charisma;
        
        public CharacterSheet(string characterName, string playerName)
        {
            this.characterName = characterName;
            this.playerName = playerName;
            RollStats();
            SetMaxHp(10);
        }
        
        public int GetMod(int abilityScore)
        {
            decimal d = (abilityScore - 10) / 2;
            return Convert.ToInt32(Math.Floor(d));
        }

        public void SetMaxHp(int baseHp)
        {
            int conMod = GetMod(constitution);
            Console.WriteLine(conMod);
            hpMax = baseHp + conMod;
            hpCurrent = hpMax;
        }

        public void RollStats()
        {
            Dice d1 = new Dice();
            strength = d1.Roll(4);
            dexterity = d1.Roll(4);
            constitution = d1.Roll(4);
            wisdom = d1.Roll(4);
            intelligence = d1.Roll(4);
            charisma = d1.Roll(4);
        }

        public string GetStats()
        {
            if (strength == 0)
                RollStats();
            string result = String.Format("Strength: {0}\nDexterity: {1}\nConstitution: {2}\nWisdom: {3}\nIntelligence: {4}\nCharisma: {5}", strength, dexterity, constitution, wisdom, intelligence, charisma);
            return result;
        }
    }
}
