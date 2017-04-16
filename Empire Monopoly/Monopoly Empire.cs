using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Empire_Monopoly.Tower;
using System.Windows;
using Empire_Monopoly.Spaces;
using System.Reflection;
using Empire_Monopoly.Properties;

namespace Empire_Monopoly
{
    public partial class MonopolyEmpires : Form
    {

        Tower.Tower tower_topleft = null;
        Tower.Tower tower_topright = null;
        Tower.Tower tower_bottomleft = null;
        Tower.Tower tower_bottomright = null;
        public Player.Player human = new Player.Player();
        public Player.Player comp1 = new Player.Player();
        public Player.Player currentPlayer = null;

        public Cards.Cards card = new Cards.Cards();
        bool loadGame = false;
        bool taketrip = false;
        public int userTurn = 1;
        int rollJailTurn = 0;

        public bool isCasinoNightCardUse = false;
        public bool isMoveToAnySpaceOnTheBoardCardUse = false;
        
        PictureBox humanToken = new PictureBox();
        PictureBox comp1Token = new PictureBox();
        PictureBox currentPlayerToken = null;
        public Dice.Dice dice = new Dice.Dice();




        enum Board_Sequence
        {

            picBxGo = 0,
            picBxNerf = 1,
            picBxRivalTowerTax = 2,
            picBxTransformers = 3,
            picBxEmpireBottom = 4,
            picBxSpotify = 5,
            picBxChanceBottom = 6,
            picBxBeats = 7,
            picBxFender = 8,
            picBxJustVisiting = 9,
            picBxJetBlue = 10,
            picBxEA = 11,
            picBxElectricCompany = 12,
            picBxHasbro = 13,
            picBxUnderArmour = 14,
            picBxChanceLeft = 15,
            picBxCarnival = 16,
            picBxYahoo = 17,
            picBxFreeParking = 18,
            picBxParamount = 19,
            picBxChevrolet = 20,
            picBxChanceTop = 21,
            picBxEbay = 22,
            picBxXGames = 23,
            picBxEmpireTop = 24,
            picBxDucati = 25,
            picBxMcdonalds = 26,
            picBxGoToJail = 27,
            picBxIntel = 28,
            picBxXBox = 29,
            picBxWaterWorks = 30,
            picBxNestle = 31,
            picBxChanceRight = 32,
            picBxSamsung = 33,
            picBxTowerTax = 34,
            picBxCocaCola = 35
        };

        public MonopolyEmpires()
        {
            InitializeComponent();




        }

        public int GetTotalDiceValue()
        {
            return Convert.ToInt32(txtRollDice1.Text) + Convert.ToInt32(txtRollDice2.Text);
        }

        private void UpdateAmount()
        {
            lblPlayer1Amount.Text = human.amount.ToString();
            lblPlayer2Amount.Text = comp1.amount.ToString();
        }

        private void MonopolyEmpires_Paint(object sender, PaintEventArgs e)
        {
            
        }



        private void pnlBoard_Paint(object sender, PaintEventArgs e)
        {
            if (loadGame == false)
            {
                tower_topleft = new Tower.Tower("Top Left", new Point(110, 210), 255, 50, 45, -45, ref pnlBoard);
                tower_topright = new Tower.Tower("Top Right", new Point(330, 210), 255, 50, 135, 45, ref pnlBoard);
                tower_bottomleft = new Tower.Tower("Bottom Left", new Point(110, 430), 255, 50, -45, 45, ref pnlBoard);
                tower_bottomright = new Tower.Tower("Bottom Right", new Point(330, 430), 255, 50, -135, 135, ref pnlBoard);

                loadGame = true;
            }


            //#region Draw Tower Top Left
            //Draw_Tower("Top Left", ref tower_topleft, 110, 210, 255, 50, 45,-45);
            //#endregion

            //#region Draw Tower Bottom Left
            //Draw_Tower("Bottom Left", ref tower_bottomleft, 110, 430, 255, 50, -45,45);

            //#endregion

            //#region Draw Tower Top Right
            //Draw_Tower("Top Right", ref tower_topright, 330, 210, 255, 50, 135,45);

            //#endregion

            //#region Draw Tower Bottom Right
            //Draw_Tower("Bottom Right", ref tower_bottomright, 330, 430, 255, 50, -135, 135);

            //#endregion



        }






        private void MonopolyEmpires_Load(object sender, EventArgs e)
        {
            //StartGame();
            
        }

        private void GetReadyComputerPlayer()
        {
            comp1.playerName = "Player 2";
            comp1.t = tower_topright;
            Image imgPlayer1 = Empire_Monopoly.Properties.Resources.Smiley;

            comp1Token.Image = imgPlayer1;
            comp1Token.SizeMode = PictureBoxSizeMode.StretchImage;
            comp1Token.BackColor = Color.Transparent;
            comp1Token.Size = new Size(40, 35);


            picBxGo.Controls.Add(comp1Token);


            Control[] obj = (this.Controls.Find("picBxGo", true));
            PictureBox pic = (PictureBox)obj[0];
            comp1Token.Location = new Point(pic.Width / 2, pic.Height / 2);

            ((PictureBox)obj[0]).Controls.Add(comp1Token);

            comp1.Current_Pos = 0;
        }

        private void GetReadyHumanPlayer()
        {
            human.playerName = "Player 1";
            human.t = tower_topleft;
            Image imgPlayer1 = Empire_Monopoly.Properties.Resources.Hourse;

            humanToken.Image = imgPlayer1;
            humanToken.SizeMode = PictureBoxSizeMode.StretchImage;
            humanToken.BackColor = Color.Transparent;
            humanToken.Size = new Size(40, 35);


            picBxGo.Controls.Add(humanToken);


            Control[] obj = (this.Controls.Find("picBxGo", true));
            PictureBox pic = (PictureBox)obj[0];
            humanToken.Location = new Point(pic.Width / 2, pic.Height / 2);

            ((PictureBox)obj[0]).Controls.Add(humanToken);

            human.Current_Pos = 0;
        }

        private void StartGame()
        {
            SetCurrentUser();
            GetReadyHumanPlayer();
            GetReadyComputerPlayer();
            EnableControl();

            lblTurn.Text = currentPlayer.playerName;
        }

        private void EnableControl()
        {
            btnRollDice.Enabled = true;
            btnGo.Enabled = true;
            btnEndTurn.Enabled = true;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void DisplayEmpirecards()
        {
            lstEmpireCard.Items.Clear();

            for (int i = 0; i < human.lst_EmpireCardName.Count; i++)
            {
                lstEmpireCard.Items.Add(human.lst_EmpireCardName[i]);
            }
        }

        private void DisplayChanceCard()
        {
            lstChanceCard.Items.Clear();
            for (int i = 0; i < human.lst_ChanceCardName.Count; i++)
            {
                lstChanceCard.Items.Add(human.lst_ChanceCardName[i]);
            }
        }

        public void UseDice()
        {

            List<int> lDiceValue = dice.RollDice();
            txtRollDice1.Text = Convert.ToString(lDiceValue[0]);
            txtRollDice2.Text = Convert.ToString(lDiceValue[1]);
            
            if(userTurn==1)
            {
                if (currentPlayer.isInJail)
                {
                    rollJailTurn++;
                    if (lDiceValue[0] == lDiceValue[1])
                    {
                        MessageBox.Show("User is free now...User can move further...!!!", "Get out of jail");
                        currentPlayer.isInJail = false;
                        lblMsg.Visible = false;
                        rollJailTurn = 0;
                        NextTurn();
                        return;
                    }
                    if (rollJailTurn == 3)
                    {
                        MessageBox.Show("User has to pat $50 to the bank...!!!", "Get out of the jail");
                        rollJailTurn = 0;
                        currentPlayer.PayBank(50);
                        lblMsg.Visible = false;
                        currentPlayer.isInJail = false;
                        GetBrandSpace();
                        CurrentBrandDecision();


                    }



                }
            }
            

        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {

            List<int> lDiceValue = dice.RollDice();
            txtRollDice1.Text = Convert.ToString(lDiceValue[0]);
            txtRollDice2.Text = Convert.ToString(lDiceValue[1]);

            if (isCasinoNightCardUse == true)
            {
                int player1DiceValue = lDiceValue[0] + lDiceValue[1];
                lDiceValue = dice.RollDice();
                int player2DiceValue = lDiceValue[0] + lDiceValue[1];

                if (player1DiceValue > player2DiceValue)
                {
                    human.amount += 200;
                }
                else
                {
                    comp1.amount += 200;
                }
                isCasinoNightCardUse = false;
                lblMsg.Visible = false;
                NextTurn();
                return;
            }


            UseDice();








        }

        private void SetCurrentUser()
        {
            if (userTurn == 1)
            {
                currentPlayer = human;
                currentPlayerToken = humanToken;
            }
            else if (userTurn == 2)
            {
                currentPlayer = comp1;
                currentPlayerToken = comp1Token;
            }
        }

        public void NextTurn()
        {
            UpdateAmount();
            userTurn++;
            if (userTurn == 3)
            {
                userTurn = 1;
            }

            SetCurrentUser();
            lblTurn.Text = currentPlayer.playerName;
            if (userTurn == 1)
            {
                currentPlayer.CheckGetOutOfJail(ref lblMsg,this);
            }
            if (userTurn == 2)
            {
                RunComp1();
            }

            

        }

        private void btnEndTurn_Click(object sender, EventArgs e)
        {

            NextTurn();


        }



        private void RunComp1()
        {
            Random r = new Random();
            int decide = 0;
            if (currentPlayer.isInJail)
            {
                decide = r.Next(1, 3);
                switch (decide)
                {
                    case 1:
                        
                        currentPlayer.amount -= 100;
                        currentPlayer.isInJail = false;
                        MessageBox.Show("User Pais $100 to bank to get out of jail...");
                        NextTurn();
                        return;

                    case 2:
                        lblMsg.Text = "Roll Double (user has three chance to roll the dice)";
                        lblMsg.Visible = true;
                        int i = 0;
                        while (i <= 3)
                        {
                            List<int> lDiceValue = dice.RollDice();
                            if(lDiceValue[0]==lDiceValue[1])
                            {
                                currentPlayer.isInJail = false;
                                lblMsg.Visible = false;

                            }
                            i++;
                        }
                        if(currentPlayer.isInJail==true)
                        {
                            currentPlayer.amount -= 50;
                            currentPlayer.isInJail = false;
                        }
                        NextTurn();
                        return;

                }
            }

            UseDice();


            GetBrandSpace();
            Board_Sequence b = (Board_Sequence)(currentPlayer.Current_Pos);
            Type type_Space = Type.GetType("Empire_Monopoly.Spaces." + b.ToString().Replace("picBx", ""));
            object o = Activator.CreateInstance(type_Space);

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

            //if (b.ToString() == "pixBxGo")
            //{
            //    NextTurn();
            //}

            if (b.ToString() == "picBxJustVisiting")
            {
                NextTurn();
            }

            else if (b.ToString() == "picBxEmpireBottom" || b.ToString() == "picBxEmpireTop")
            {
                card.DecideEmpireCard(this);
                DisplayEmpirecards();
                NextTurn();
            }
            else if (b.ToString() == "picBxChanceBottom" || b.ToString() == "picBxChanceLeft" || b.ToString() == "picBxChanceRight" || b.ToString() == "picBxChanceTop")
            {
                card.DecideChanceCard(this);
                DisplayChanceCard();
                if (isCasinoNightCardUse == false)
                {
                    NextTurn();
                }
            }
            //else if (b.ToString() == "picBxElectricCompany")
            //{
            //    NextTurn();
            //}
            //else if (b.ToString() == "picBxWaterWorks")
            //{
            //    NextTurn();
            //}
            else if (b.ToString() == "picBxFreeParking")
            {
                if (r.Next(1, 3) == 1)
                {


                    int pos = r.Next(0, 36);
                    Control[] p = this.Controls.Find(((Board_Sequence)(pos)).ToString(), true);
                    MoveToken(p[0] as PictureBox);

                    currentPlayer.Current_Pos = pos;
                    currentPlayer.amount -= 100;
                    RunComp1();
                    
                }
                NextTurn();

            }
            else if (b.ToString() == "picBxTowerTax")
            {

                currentPlayer.PayTax(ref pnlBoard);
                NextTurn();
            }
            else if (b.ToString() == "picBxRivalTowerTax")
            {
                human.PayTax(ref pnlBoard);
                NextTurn();
            }
            else if (b.ToString() == "picBxGoToJail")
            {
                GoToJail();
                NextTurn();
            }
            else
            {

                if (human.brandNames.Contains("," + b.ToString().Replace("picBx", "") + ","))
                {
                    Board_Sequence b_ownedBrand = (Board_Sequence)(human.Current_Pos);
                    currentPlayer.PayRent(human, b_ownedBrand.ToString());
                    NextTurn();

                }

                if (currentPlayer.amount < price)
                {
                    MessageBox.Show("User doesn't have sufficient balance to purchase this brand...!!!");
                    NextTurn();
                    return;
                }

                decide = r.Next(1, 3);

                switch (decide)
                {
                    case 1:
                        MessageBox.Show("user has decided to buy the brand...");
                        currentPlayer.BuyBrand(billBoardSize, price, img, b.ToString().Replace("picBx", ""), ref pnlBoard,false);
                        NextTurn();
                        break;

                    case 2:
                        MessageBox.Show("user doesn't want to buy the brand...");
                        NextTurn();
                        return;

                }
                


            }

            

        }



        public void CurrentBrandDecision()
        {


            Board_Sequence b = (Board_Sequence)(currentPlayer.Current_Pos);
            Type type_Space = Type.GetType("Empire_Monopoly.Spaces." + b.ToString().Replace("picBx", ""));
            object o = Activator.CreateInstance(type_Space);

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


            //if (b.ToString() == "pixBxGo")
            //{

            //}

            if (b.ToString() == "picBxJustVisiting")
            {
                NextTurn();
            }

            else if (b.ToString() == "picBxEmpireBottom" || b.ToString() == "picBxEmpireTop")
            {
                card.DecideEmpireCard(this);
                DisplayEmpirecards();
                NextTurn();
                //if (isMoveToAnySpaceOnTheBoardCardUse == false)
                //{
                //    NextTurn();
                //}
                
            }
            else if (b.ToString() == "picBxChanceBottom" || b.ToString() == "picBxChanceLeft" || b.ToString() == "picBxChanceRight" || b.ToString() == "picBxChanceTop")
            {


                card.DecideChanceCard(this);
                DisplayChanceCard();
                if (isCasinoNightCardUse == false)
                {
                    NextTurn();
                }
            }
            //else if (b.ToString() == "picBxElectricCompany")
            //{
            //    NextTurn();
            //}
            //else if (b.ToString() == "picBxWaterWorks")
            //{
            //    NextTurn();
            //}
            else if (b.ToString() == "picBxFreeParking")
            {
                TakeTrip();
                NextTurn();
            }
            else if (b.ToString() == "picBxTowerTax")
            {
                currentPlayer.PayTax(ref pnlBoard);
                NextTurn();
            }
            else if (b.ToString() == "picBxRivalTowerTax")
            {
                comp1.PayTax(ref pnlBoard);
                NextTurn();
            }
            else if (b.ToString() == "picBxGoToJail")
            {
                GoToJail();
                NextTurn();
            }
            else
            {

                if (comp1.brandNames.Contains("," + b.ToString().Replace("picBx", "") + ","))
                {
                    Board_Sequence b_ownedBrand = (Board_Sequence)(comp1.Current_Pos);
                    currentPlayer.PayRent(comp1, b_ownedBrand.ToString());
                    NextTurn();
                    return;
                }

                if (currentPlayer.amount < price)
                {
                    MessageBox.Show("You don't have sufficient balance to purchase this brand...!!!");
                    NextTurn();
                    return;
                }

                if (MessageBox.Show("Would you like to buy this brand for $" + price.ToString() + "??", "Buy Brand??", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {


                    currentPlayer.BuyBrand(billBoardSize, price, img, b.ToString().Replace("picBx", ""), ref pnlBoard, false);



                }
                NextTurn();
                
            }

            
        }

        public void BuyBrandForChanceCard(int billBoardSize, int price, Image img, string brandName, bool isFree)
        {
            currentPlayer.BuyBrand(billBoardSize, price, img, "ElectricCompany", ref pnlBoard, isFree);
        }

        public void GoToJail()
        {
            MessageBox.Show("User is in jail...!!!");
            currentPlayer.Current_Pos = (int)Enum.Parse(typeof(Board_Sequence), "picBxJustVisiting");
            Control[] obj = (this.Controls.Find("picBxJustVisiting", true));
            MoveToken((PictureBox)obj[0]);
            currentPlayer.isInJail = true;
        }

        public void StockMarketCrash()
        {
            human.PayTax(ref pnlBoard);
            comp1.PayTax(ref pnlBoard);

        }

        private void btnGo_Click(object sender, EventArgs e)
        {

            GetBrandSpace();
            CurrentBrandDecision();
        }

        private void TakeTrip()
        {
            if (MessageBox.Show("Take a trip...??", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                taketrip = true;
                lblMsg.Text = "Double-Click on Space in which you want to move...!!!";
                lblMsg.Visible = true;

            }
        }

        private void PicBx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblMsg.Visible = false;
            PictureBox p = sender as PictureBox;
            MoveToken(p);
            int pos = (int)Enum.Parse(typeof(Board_Sequence), p.Name);
            if (taketrip == true)
            {
                                
                currentPlayer.Current_Pos = pos;
                currentPlayer.amount -= 100;
                CurrentBrandDecision();
                taketrip = false;
            }
            else if (isMoveToAnySpaceOnTheBoardCardUse == true)
            {

                SetTokenPossition(pos);
                isMoveToAnySpaceOnTheBoardCardUse = false;
                CurrentBrandDecision();
                
            }


        }

        public void SetLabelMessage(string str)
        {
            lblMsg.Text = str;
            lblMsg.Visible = true;
        }


        private void GetBrandSpace()
        {
            currentPlayer.Current_Pos += Convert.ToInt32(txtRollDice1.Text) + Convert.ToInt32(txtRollDice2.Text);

            if (currentPlayer.Current_Pos == 36)
            {
                currentPlayer.Current_Pos = 0;
                currentPlayer.amount += currentPlayer.topMostBillBoardValue;
            }
            else if (currentPlayer.Current_Pos > 35)
            {
                currentPlayer.Current_Pos -= 36;
                AddGoSpaceAmount();
            }
            SetTokenPossition(currentPlayer.Current_Pos);
        }


        public void SetTokenPossition(int index)
        {
            Board_Sequence b = (Board_Sequence)(index);
            Control[] obj = (this.Controls.Find(b.ToString(), true));
            MoveToken((PictureBox)obj[0]);
        }
        

        public void AddGoSpaceAmount()
        {
            currentPlayer.amount += currentPlayer.topMostBillBoardValue;
        }

        private void MoveToken(PictureBox pic)
        {



            currentPlayerToken.Location = new Point(pic.Width / 2, pic.Height / 2);
            pic.Controls.Add(currentPlayerToken);
        }


        





        private void button1_Click(object sender, EventArgs e)
        {

            currentPlayer.BuyBrand(1, 100, Empire_Monopoly.Properties.Resources.BB_Beats, "Beats", ref pnlBoard,false);
            currentPlayer.BuyBrand(2, 150, Empire_Monopoly.Properties.Resources.BB_Ea, "EA", ref pnlBoard,false);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Image img = currentPlayer.t.towerBillBoard.Keys.ElementAt(currentPlayer.t.towerBillBoard.Count - 1);

            Point bPoint = (currentPlayer.t.towerBillBoard[img] as Tuple<Point, int>).Item1;
            int billBoardSize = (currentPlayer.t.towerBillBoard[img] as Tuple<Point, int>).Item2;


            currentPlayer.RemoveBillBoard(bPoint, billBoardSize);
            // tower_topleft.billBoardPoint[bPoint] = false;

            currentPlayer.t.towerBillBoard.Remove(img);

            currentPlayer.RefreshTower(currentPlayer.t.towerName, currentPlayer.t.original_topLeft, currentPlayer.t.width, currentPlayer.t.height, currentPlayer.t.angle, currentPlayer.t.billBoardAngle, ref pnlBoard);


        }

        private void btnUseEmpireCard_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(lstEmpireCard.SelectedItem) == string.Empty)
            {
                return;
            }


            card.PlayEmpireCard(this, Convert.ToString(lstEmpireCard.SelectedItem));
            UpdateAmount();

        }

        private void btnUseChanceCard_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(lstChanceCard.SelectedItem) == string.Empty)
            {
                return;
            }


            card.PlayChanceCard(this, Convert.ToString(lstChanceCard.SelectedItem));
            UpdateAmount();
        }

        

       










    }
}
