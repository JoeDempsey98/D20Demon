﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace DiscordDnDBot
{
    class CharacterSheet
    {
        public string characterName;
        public string playerName;
        public string characterClass;
        public int hpMax;
        public int hpCurrent;
        public int strength;
        public int dexterity;
        public int constitution;
        public int wisdom;
        public int intelligence;
        public int charisma;

        int strMod;
        int dexMod;
        int conMod;
        int wisMod;
        int intMod;
        int chaMod;
        
        public CharacterSheet(string characterName, string playerName)
        {
            this.characterName = characterName;
            this.playerName = playerName;
        }
        /*public CharacterSheet(string characterName, string playerName, string characterClass)
        {
            this.characterName = characterName;
            this.playerName = playerName;
            this.characterClass = characterClass;
        }*/

        public void SetMaxHp(int baseHp)
        {
            decimal d = (constitution - 10) / 2;
            conMod = Convert.ToInt32(Math.Floor(d));
            Console.WriteLine(conMod);
            hpMax = baseHp + conMod;
        }
        public void RollStats()
        {
            Dice d1 = new Dice();
            strength = d1.Roll(3);
            dexterity = d1.Roll(3);
            constitution = d1.Roll(3);
            wisdom = d1.Roll(3);
            intelligence = d1.Roll(3);
            charisma = d1.Roll(3);
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
