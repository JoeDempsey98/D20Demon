using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordDnDBot
{
    class Dice
    {
        Random rand = new Random();
        int sides = 6;

        public Dice()
        {

        }
        public Dice(int sides)
        {
            this.sides = sides;
        }

        public int Roll(int num = 1)
        {
            if (num == 1)
                return rand.Next(1, sides + 1);
            return rand.Next(1, sides + 1) + Roll(num - 1);
        }
    }
}
