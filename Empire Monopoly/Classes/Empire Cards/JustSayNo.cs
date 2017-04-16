using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Empire_Cards
{
    class JustSayNo : Cards
    {
        public void Action(MonopolyEmpires Me)
        {
            Me.NextTurn();
        }
    }
}
