using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Chance_Cards
{
    class CasinoNight : Cards
    {

        public void Action(MonopolyEmpires Me)
        {
            Me.isCasinoNightCardUse = true;
            Me.SetLabelMessage("Roll dice to play CasinoNight Card...");
            //Me.UseDice();
            
        }

    }
}
