using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Drawing;

namespace Empire_Monopoly.Cards.Chance_Cards
{
    class WaterBonus : Cards
    {

        public void Action(MonopolyEmpires Me)
        {
            Type type_Space = Type.GetType("Empire_Monopoly.Spaces.WaterWorks");
            object o = Activator.CreateInstance(type_Space);

            FieldInfo F_Count = type_Space.GetField("count");
            int count = 0;

            if (F_Count != null)
            {
                count = (int)F_Count.GetValue(o);
            }

            
            if (count > 0)
            {
                FieldInfo f = type_Space.GetField("img");
                Image img = null;
                if (f != null)
                {
                    img = f.GetValue(null) as Image;
                }

                FieldInfo F_Size = type_Space.GetField("size");
                int billBoardSize = 0;
                if (F_Size != null)
                {
                    billBoardSize = (int)F_Size.GetValue(o);
                }

                FieldInfo F_Price = type_Space.GetField("price");
                int price = 0;
                if (F_Price != null)
                {
                    price = (int)F_Price.GetValue(o);
                }

                Me.BuyBrandForChanceCard(billBoardSize, price, img, "WaterWorks.cs", true);
            }

            F_Count.SetValue(o, count--);



        }
        

    }
}
