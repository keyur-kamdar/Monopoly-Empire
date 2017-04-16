using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Empire_Cards
{
    class BillBoardTax:Cards
    {


        public void Action(MonopolyEmpires Me)
        {
            if (Me.userTurn == 1)
            {
                if (Me.comp1.t.towerBillBoard.Count > 0)
                {
                    Me.human.amount += 400;
                    Me.comp1.amount -= 400;
                }
            }
            else if(Me.userTurn==2)
            {
                if (Me.human.t.towerBillBoard.Count > 0)
                {
                    Me.comp1.amount += 400;
                    Me.human.amount -= 400;
                }
            }
        }
    }
}
