using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Empire_Monopoly.Cards
{
    public class Cards
    {
        private int chanceCard = 0;
        private int empireCard = 0;

        List<string> lst_ChanceCard_Sequence = null;
        List<string> lst_EmpireCard_Sequence = null;


        public Cards()
        {

            
            string[] str_Chance = { "BusinessOfTheYearAward", "CasinoNight", "EmpireBDay", "GetOutOfJailFree", "GoToJail", "GoToJailForFraud", "InsiderTradingFine", "LaunchYourWebSite", "ProfitSoar", "SolarPowerBonus", "SpeedAhead", "SuccessfulAdvertisingCampaign", "WaterBonus", "CasinoNight" };

            lst_ChanceCard_Sequence = new List<string>(str_Chance);

            string[] str_Empire = {  "MoveToAnySpaceOnTheBoard", "PrivateJetTrip", "StockMarketCrash", "TallestTowerBonus","BillBoardTax", "GetOutOfJailForFree", "JustSayNo", "JustSayNo", "JustSayNo" };

            //string[] str_Empire = { "BargainBusiness", "BillBoardTax", "DealBuster", "GetOutOfJailForFree", "HostileTakeover", "JustSayNo", "MoveToAnySpaceOnTheBoard", "PrivateJetTrip", "ReverseRent", "StockMarketCrash", "JustSayNo", "SwapTheTop", "TallestTowerBonus", "JustSayNo" };
            lst_EmpireCard_Sequence = new List<string>(str_Empire);


        }


        public int NextChanceCard()
        {
            chanceCard++;
            if (chanceCard == 15)
            {
                chanceCard = 0;
            }
            return chanceCard;
        }

        public int NextEmpireCard()
        {
            empireCard++;
            if (empireCard == 15)
            {
                empireCard = 0;
            }
            return empireCard;
        }

        public void DecideChanceCard(MonopolyEmpires Me)
        {


            MethodInfo method = null;
            object o_ChanceCard = null;

            int cardNum = NextChanceCard();
            string ChanceCardName = lst_ChanceCard_Sequence[cardNum - 1];
            Type type_ChanceCard = Type.GetType("Empire_Monopoly.Cards.Chance_Cards." + ChanceCardName);
            //Type type_ChanceCard = Type.GetType("Empire_Monopoly.Cards.Chance_Cards.EmpireBDay");

            MessageBox.Show("Selected Chance Card is:" + ChanceCardName);
            o_ChanceCard = Activator.CreateInstance(type_ChanceCard);

            method = type_ChanceCard.GetMethod("Action");


            if (o_ChanceCard.ToString().Contains("GetOutOfJailFree"))
            {
                Me.currentPlayer.lst_ChanceCardName.Add("GetOutOfJailFree");
            }
            else
            {
                method.Invoke(o_ChanceCard, new object[] { Me });
            }



        }

        public void PlayChanceCard(MonopolyEmpires Me, string chanceCardName)
        {
            MethodInfo method = null;


            int cardNum = NextChanceCard();
            string ChnaceCardName = lst_ChanceCard_Sequence[cardNum - 1];
            Type type_ChanceCard = Type.GetType("Empire_Monopoly.Cards.Chance_Cards." + ChnaceCardName);
            //o_EmpireCard = Activator.CreateInstance(type_EmpireCard);

            method = type_ChanceCard.GetMethod("Action");

            Me.currentPlayer.lst_ChanceCardName.Remove(ChnaceCardName);


        }


        public void DecideEmpireCard(MonopolyEmpires Me)
        {

            int cardNum = NextEmpireCard();
            string EmpireCardName = lst_EmpireCard_Sequence[cardNum - 1];
            Type type_EmpireCard = Type.GetType("Empire_Monopoly.Cards.Empire_Cards." + EmpireCardName);
            
            //o_EmpireCard = Activator.CreateInstance(type_EmpireCard);

            //method = type_EmpireCard.GetMethod("Action");

            MessageBox.Show("Selected Empire Card is:" + EmpireCardName);

            Me.currentPlayer.lst_EmpireCardName.Add(EmpireCardName);




        }

        
        public void PlayEmpireCard(MonopolyEmpires Me, string empireCardName)
        {
            MethodInfo method = null;
            object o_EmpireCard = null;
            Type type_EmpireCard = Type.GetType("Empire_Monopoly.Cards.Empire_Cards." + empireCardName);
            o_EmpireCard = Activator.CreateInstance(type_EmpireCard);

            method = type_EmpireCard.GetMethod("Action");

            Me.currentPlayer.lst_EmpireCardName.Remove(empireCardName);
            method.Invoke(o_EmpireCard, new object[] { Me });
           
        }


    }
}
