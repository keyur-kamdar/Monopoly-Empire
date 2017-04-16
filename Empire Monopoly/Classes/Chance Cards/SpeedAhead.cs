using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Chance_Cards
{
    class SpeedAhead : Cards
    {


        public void Action(MonopolyEmpires Me)
        {
            Me.currentPlayer.Current_Pos += 5;
            if (Me.currentPlayer.Current_Pos == 36)
            {
                Me.currentPlayer.Current_Pos = 0;
                Me.currentPlayer.amount += Me.currentPlayer.topMostBillBoardValue;
            }
            else if (Me.currentPlayer.Current_Pos > 35)
            {
                Me.currentPlayer.Current_Pos -= 36;
                Me.AddGoSpaceAmount();

            }
            Me.SetTokenPossition(Me.currentPlayer.Current_Pos);
            Me.CurrentBrandDecision();
        }
    }
}
