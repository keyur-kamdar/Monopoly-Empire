﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Empire_Monopoly.Cards.Chance_Cards
{
    class GoToJailForFraud : Cards
    {

        public void Action(MonopolyEmpires Me)
        {
            Me.GoToJail();
        }
    }
}
