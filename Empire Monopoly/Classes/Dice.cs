using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Dice
{
    public class Dice
    {

        int dice_value1 = 0;
        int dice_value2 = 0;

        public List<int> RollDice()
        {
            List<int> lDiceValue = new List<int>();
            Random r = new Random();
            dice_value1 = r.Next(1, 7);
            dice_value2 = r.Next(1, 7);
            lDiceValue.Add(dice_value1);
            lDiceValue.Add(dice_value2);

            return lDiceValue;
        }
    }
}
