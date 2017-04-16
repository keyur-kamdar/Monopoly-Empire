using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Empire_Cards
{
    class MoveToAnySpaceOnTheBoard : Cards
    {

        public void Action(MonopolyEmpires Me)
        {
            Me.isMoveToAnySpaceOnTheBoardCardUse = true;
            Me.SetLabelMessage("Double click on the brand on which you want to move...");
        }

    }
}
