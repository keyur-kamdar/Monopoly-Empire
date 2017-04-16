using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Empire_Cards
{
    class TallestTowerBonus : Cards
    {

        public void Action(MonopolyEmpires Me)
        {
            int a = 0;
            if (Me.human.amount >= Me.comp1.amount)
            {
                a = Me.human.amount;

            }
            else
            {
                a = Me.comp1.amount;
            }
            Me.currentPlayer.amount += a;
        }
    }
}
