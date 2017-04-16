using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Empire_Monopoly.Player
{
    public class Player
    {

        
        public string playerName { get; set; }

        public int Current_Pos { get; set; }

        public int topMostBillBoardValue = 50;

        public Tower.Tower t = null;

        public int amount = 1000;

        public string brandNames = ",";

        public bool isInJail = false;

        public List<string> lst_ChanceCardName = new List<string>();
        public List<string> lst_EmpireCardName = new List<string>();

        public Player()
        {

        }

        public void BuyBrand(int billBoardSize,int price,Image img,string brandName, ref Panel pnlBoard,bool isFree)
        {

            

            Point bPoint = new Point();
            int tempBillBoardSize = billBoardSize;
            if (t.towerBillBoard.Count == 0)
            {
                if (tempBillBoardSize == 1)
                {
                    bPoint = t.billBoardPoint.Keys.ElementAt(0);
                    t.billBoardPoint[bPoint] = true;
                }
                else
                {
                    Dictionary<Point, bool> tempBillBoardPoint = new Dictionary<Point, bool>(t.billBoardPoint);

                    foreach (KeyValuePair<Point, bool> obj in tempBillBoardPoint)
                    {
                        if (tempBillBoardSize > 0)
                        {
                            bPoint = obj.Key;
                            t.billBoardPoint[obj.Key] = true;
                            tempBillBoardSize--;
                        }
                        else
                        {
                            break;
                        }

                    }
                }

            }
            else
            {
                Dictionary<Point, bool> tempBillBoardPoint = new Dictionary<Point, bool>(t.billBoardPoint);
                foreach (KeyValuePair<Point, bool> obj in tempBillBoardPoint)
                {
                    if (obj.Value == false)
                    {

                        if (tempBillBoardSize > 0)
                        {
                            bPoint = obj.Key;
                            t.billBoardPoint[obj.Key] = true;
                            tempBillBoardSize--;
                        }
                        else
                        {
                            break;
                        }



                    }
                }
            }
            //Image img = new Bitmap(Empire_Monopoly.Properties.Resources.Nerf, 50, 14);
            t.SetBillBoardOnTheTower(ref pnlBoard, new Bitmap(img, 50, 14 * billBoardSize), bPoint, billBoardSize);
            int count = 0;
            foreach (KeyValuePair<Point, bool> k in t.billBoardPoint)
            {
                if (k.Value == true)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            topMostBillBoardValue = t.amount_Seq[count];
            if (isFree == false)
            {
                amount -= price;
            }
            this.brandNames += brandName + ",";

            bool isWin = true;
            foreach(KeyValuePair<Point,bool> k in t.billBoardPoint )
            {
                if(k.Value==false)
                {
                    isWin = false;
                }
            }
            if (t.billBoardPoint.Count == 0)
            {
                isWin = false;
            }



            if (isWin == true)
            {
                MessageBox.Show(playerName + " is win the game");
                Application.Exit();

                return;
            }
        }

        public void PayRent(Player ownedPlayer,string spaceName)
        {
            Type type_Space = Type.GetType("Empire_Monopoly.Spaces." + spaceName.Replace("picBx", ""));
            object o = Activator.CreateInstance(type_Space);

            //FieldInfo F_OwnerPlayerBillBoardAmount = type_Space.GetField("topMostBillBoardValue");
            //int OwnerPlayer_topMostBillBoardValue = (int)F_OwnerPlayerBillBoardAmount.GetValue(o);
            if (amount < ownedPlayer.topMostBillBoardValue)
            {
                MessageBox.Show("User doesn't have sufficient balance to pay rent...!!!");
                return;
            }

            MessageBox.Show("User has to pay ...!!!");
            amount -= ownedPlayer.topMostBillBoardValue;
            ownedPlayer.amount += ownedPlayer.topMostBillBoardValue;

            

        }

        public void PayTax(ref Panel pnlBoard)
        {

            if(t.towerBillBoard.Count==0)
            {
                MessageBox.Show("User doesn't has any brand to pay...!!!");
                return;
            }
            Image img = t.towerBillBoard.Keys.ElementAt(t.towerBillBoard.Count - 1);

            Point bPoint = (t.towerBillBoard[img] as Tuple<Point, int>).Item1;
            int billBoardSize = (t.towerBillBoard[img] as Tuple<Point, int>).Item2;


            RemoveBillBoard(bPoint, billBoardSize);
            // tower_topleft.billBoardPoint[bPoint] = false;

            t.towerBillBoard.Remove(img);

            RefreshTower(t.towerName, t.original_topLeft, t.width, t.height, t.angle, t.billBoardAngle, ref pnlBoard);
        }


        public void RemoveBillBoard(Point bPoint, int billBoardSize)
        {
            bool check = false;
            Dictionary<Point, bool> tempBillBoarPoint = new Dictionary<Point, bool>(t.billBoardPoint);
            foreach (KeyValuePair<Point, bool> item in tempBillBoarPoint.Reverse())
            {
                if (item.Key == bPoint || (check == true && billBoardSize > 0))
                {
                    t.billBoardPoint[item.Key] = false;
                    billBoardSize--;
                    check = true;
                }
            }

        }

        public void RefreshTower(string towerName, Point p, int width, int height, int angle, int billBoardAngle, ref Panel pnlBoard)
        {
            // Draw_Tower("Top Left", ref tower_topleft, 110, 210, 255, 50, 45,-45);
            
            t.RedrawTower(towerName,p, width, height, angle, billBoardAngle, ref pnlBoard);
            foreach (KeyValuePair<Image, Tuple<Point, int>> item in t.towerBillBoard)
            {
                Image img = item.Key;

                Point bPoint = (item.Value as Tuple<Point, int>).Item1;

                int billboardSize = (item.Value as Tuple<Point, int>).Item2;
                t.SetBillBoardOnTheTower(ref pnlBoard, img, bPoint);


            }

        }

        public void CheckGetOutOfJail(ref Label lblMsg,MonopolyEmpires Me)
        {
            if (isInJail)
            {
                if (lst_ChanceCardName.Contains("GetOutOfJailFree"))
                {
                    if(MessageBox.Show("Would you like to use Chance Card to get out of jail...??", "Get out Of jail", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Me.card.PlayChanceCard(Me, "GetOutOfJailFree");
                        Me.NextTurn();
                        return;
                    }
                }
                if (lst_EmpireCardName.Contains("GetOutOfJailForFree"))
                {
                    if (MessageBox.Show("Would you like to use Empire Card to get out of jail...??", "Get out Of jail", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Me.card.PlayEmpireCard(Me, "GetOutOfJailForFree");
                        Me.NextTurn();
                        return;
                    }
                }
                if (MessageBox.Show("Would you like to pay $100 to bank to get out of jail...??", "Get out Of jail", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    amount -= 100;
                    isInJail = false;
                    Me.NextTurn();
                    return;
                }
                if (MessageBox.Show("Want to roll dice to get out of jail...??", "Get out of jail", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    lblMsg.Text = "Roll Double (user has three chance to roll the dice)";
                    lblMsg.Visible = true;
                }



            }
        }

        public void PayBank(int amt)
        {
            amount -= amt;
        }

    }
}
