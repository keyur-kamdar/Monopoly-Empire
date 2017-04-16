using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Chance_Cards
{
    class EmpireBDay : Cards
    {


        public void Action(MonopolyEmpires Me)
        {
            Me.currentPlayer.amount += 50;
            if(Me.userTurn==1)
            {
                
                Me.comp1.amount -= 50;
            }
            else if(Me.userTurn==2)
            {
                
                Me.human.amount -= 50;
            }

        }
    }
}
