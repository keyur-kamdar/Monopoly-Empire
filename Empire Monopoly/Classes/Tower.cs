using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Empire_Monopoly.Tower
{
    public class Tower
    {
        public string towerName { get; set; }

        public Point original_topLeft { get; set; }

        public Point topLeft { get; set; }

        public Point topRight { get; set; }

        public Point bottomLeft { get; set; }

        public Point bottomRight { get; set; }

        public Point center { get; set; }

        public int angle { get; set; }

        public int billBoardAngle { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public bool towerSet = false;

        public int[] amount_Seq = { 50, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800 };

        public Dictionary<Point, bool> billBoardPoint = new Dictionary<Point, bool>();

        public Dictionary<Image, Tuple<Point, int>> towerBillBoard = new Dictionary<Image, Tuple<Point, int>>();

        public Tower(string towerName, Point p, int width, int height, int angle, int billBoardAngle,ref Panel pnlBoard)
        {
            this.towerName = towerName;
            this.original_topLeft = p;
            this.width = width;
            this.height = height;
            this.angle = angle;
            this.billBoardAngle = billBoardAngle;
            this.center = new Point(Convert.ToInt32(original_topLeft.X + this.width / 2), Convert.ToInt32(original_topLeft.Y + this.height / 2));
            this.topLeft = rotatePoint(original_topLeft);
            this.topRight = rotatePoint(new Point(original_topLeft.X + this.width, original_topLeft.Y));
            this.bottomLeft = rotatePoint(new Point(original_topLeft.X, original_topLeft.Y + this.height));
            this.bottomRight = rotatePoint(new Point(original_topLeft.X + this.width, original_topLeft.Y + this.height));
            Draw_Tower(ref pnlBoard);
        }

        public void SetBillBoardOnTheTower(ref Panel pnlBoard, Image img, Point bPoint, int billBoardSize = 0)
        {


            //Point p = new Point(300, 333);

            if (!towerBillBoard.ContainsKey(img))
            {
                Tuple<Point, int> t = new Tuple<Point, int>(bPoint, billBoardSize);
                towerBillBoard.Add(img, t);
            }

            RotateImage(img, bPoint, billBoardAngle, ref pnlBoard);
        }

        public void RotateImage(Image image, PointF offset, float angle,ref Panel pnlBoard)
        {

            //PictureBox p1 = new PictureBox();
            //p1.Image = RotateImage(image, new Point(image.Width / 2, image.Height / 2), angle);
            //p1.Location = new Point(Convert.ToInt32(offset.X), Convert.ToInt32(offset.Y));
            ////p1.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
            //pnlBoard.Controls.Add(p1);
            Graphics g = pnlBoard.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            Matrix m = new Matrix();


            m.RotateAt(angle, offset);

            g.Transform = m;


            g.DrawImage(image, offset);


        }


        public void RedrawTower(string towerName, Point p, int width, int height, int angle, int billBoardAngle, ref Panel pnlBoard)
        {
            this.towerName = towerName;
            this.original_topLeft = p;
            this.width = width;
            this.height = height;
            this.angle = angle;
            this.billBoardAngle = billBoardAngle;
            this.center = new Point(Convert.ToInt32(original_topLeft.X + this.width / 2), Convert.ToInt32(original_topLeft.Y + this.height / 2));
            this.topLeft = rotatePoint(original_topLeft);
            this.topRight = rotatePoint(new Point(original_topLeft.X + this.width, original_topLeft.Y));
            this.bottomLeft = rotatePoint(new Point(original_topLeft.X, original_topLeft.Y + this.height));
            this.bottomRight = rotatePoint(new Point(original_topLeft.X + this.width, original_topLeft.Y + this.height));
            Draw_Tower(ref pnlBoard);
        }

        public void RemoveImage(ref Panel pnlBoard, ref ImageList imglist)
        {

            //Bitmap img = towerBillBoard.Keys.ElementAt(0) as Bitmap;
            //Graphics g = pnlBoard.CreateGraphics();
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //Matrix m = new Matrix();


            //m.RotateAt(angle, offset);

            //g.Transform = m;
            
            //imglist.Images.RemoveAt(0);
            
        }



        private void Draw_Tower(ref Panel pnlBoard)
        {



            
            Graphics g = pnlBoard.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Rectangle r = new System.Drawing.Rectangle(original_topLeft.X, original_topLeft.Y, width, height);

            Matrix m = new Matrix();
            int i_50 = 0;
            //m.RotateAt(angle, new PointF(r.Left , r.Top ));
            m.RotateAt(angle, new PointF(r.Left + (r.Width / 2), r.Top + (r.Height / 2)));

            g.Transform = m;


            #region working Code
            //int i1 = t.bottomLeft.X;
            //int j1 = t.bottomLeft.Y;
            //int i2 = t.topLeft.X;
            //int j2 = t.topLeft.Y;

            //g.DrawRectangle(Pens.Black, r);

            //g.ResetTransform();

            //int towerValue = 800;


            //g.DrawLine(Pens.Red, i1, j1, i2, j2);
            //Label lb = new Label();


            //if (t.towerName == "Bottom Right")
            //{
            //    while (i1 > t.bottomRight.X || j1 > t.bottomRight.Y || i2 > t.topRight.X || j2 > t.topRight.Y)
            //    {
            //        i1 += Convert.ToInt32(14 * Math.Cos(t.angle * Math.PI / 180));
            //        j1 += Convert.ToInt32(14 * Math.Sin(t.angle * Math.PI / 180));
            //        i2 += Convert.ToInt32(14 * Math.Cos(t.angle * Math.PI / 180));
            //        j2 += Convert.ToInt32(14 * Math.Sin(t.angle * Math.PI / 180));

            //        Display_TowerValue(t.towerName, ref g, ref towerValue, ref m, i1, j1, ref i_50);

            //        g.DrawLine(Pens.Black, i1, j1, i2, j2);
            //        t.billBoardPoint.Add(new Point(i1, j1), false);
            //    }
            //}
            //else
            //{


            //    while (i1 < t.bottomRight.X || j1 < t.bottomRight.Y || i2 < t.topRight.X || j2 < t.topRight.Y)
            //    {
            //        i1 += Convert.ToInt32(14 * Math.Cos(t.angle * Math.PI / 180));
            //        j1 += Convert.ToInt32(14 * Math.Sin(t.angle * Math.PI / 180));
            //        i2 += Convert.ToInt32(14 * Math.Cos(t.angle * Math.PI / 180));
            //        j2 += Convert.ToInt32(14 * Math.Sin(t.angle * Math.PI / 180));

            //        Display_TowerValue(t.towerName, ref g, ref towerValue, ref m, i1, j1, ref i_50);

            //        g.DrawLine(Pens.Black, i1, j1, i2, j2);
            //        t.billBoardPoint.Add(new Point(i1, j1), false);
            //    }
            //}
            #endregion


            int i1 = bottomRight.X;
            int j1 = bottomRight.Y;
            int i2 = topRight.X;
            int j2 = topRight.Y;


            Brush b = new SolidBrush(pnlBoard.BackColor);
            g.FillRectangle(b, r);

            g.DrawRectangle(Pens.White, r);

            g.ResetTransform();

            int towerValue = 50;


            g.DrawLine(Pens.Red, i1, j1, i2, j2);
            Label lb = new Label();


            if (towerName == "Bottom Right")
            {
                while (i1 < bottomLeft.X || j1 < bottomLeft.Y || i2 < topLeft.X || j2 < topLeft.Y)
                {
                    i1 -= Convert.ToInt32(14 * Math.Cos(angle * Math.PI / 180));
                    j1 -= Convert.ToInt32(14 * Math.Sin(angle * Math.PI / 180));
                    i2 -= Convert.ToInt32(14 * Math.Cos(angle * Math.PI / 180));
                    j2 -= Convert.ToInt32(14 * Math.Sin(angle * Math.PI / 180));

                    Display_TowerValue(ref g, ref towerValue, ref m, i1, j1, ref i_50, pnlBoard.Font, pnlBoard.ForeColor);

                    g.DrawLine(Pens.White, i1, j1, i2, j2);
                    if (towerSet == false)
                    {
                        billBoardPoint.Add(new Point(i1, j1), false);
                    }

                }
            }
            else
            {


                while (i1 > bottomLeft.X || j1 > bottomLeft.Y || i2 > topLeft.X || j2 < topLeft.Y)
                {
                    i1 -= Convert.ToInt32(14 * Math.Cos(angle * Math.PI / 180));
                    j1 -= Convert.ToInt32(14 * Math.Sin(angle * Math.PI / 180));
                    i2 -= Convert.ToInt32(14 * Math.Cos(angle * Math.PI / 180));
                    j2 -= Convert.ToInt32(14 * Math.Sin(angle * Math.PI / 180));

                    Display_TowerValue(ref g, ref towerValue, ref m, i1, j1, ref i_50, pnlBoard.Font, pnlBoard.ForeColor);

                    g.DrawLine(Pens.White, i1, j1, i2, j2);
                    if (towerSet == false)
                    {
                        billBoardPoint.Add(new Point(i1, j1), false);

                    }
                }
            }

            towerSet = true;


        }
        private Point rotatePoint(Point p)
        {
            
            if (angle == 0) return p;
            // make the origin essentially zero:
            var px = p.X - center.X;
            var py = p.Y - center.Y;
            if (px == 0 && py == 0) return p;
            var rad = angle * Math.PI / 180;
            var cosine = Math.Cos(rad);
            var sine = Math.Sin(rad);
            p.X = Convert.ToInt32(cosine * px - sine * py);
            p.Y = Convert.ToInt32(sine * px + cosine * py);
            // put the point back:
            p.X += center.X;
            p.Y += center.Y;
            return p;
        }

        

        private void Display_TowerValue(ref Graphics g, ref int towerValue, ref Matrix m, int x, int y, ref int i_50,Font f,Color col)
        {
            m = new Matrix();
            switch (towerName)
            {
                case "Top Left":
                    m.RotateAt(-45, new PointF(x, y));
                    break;
                case "Bottom Left":
                    m.RotateAt(-135, new PointF(x, y));
                    break;

                case "Top Right":
                    m.RotateAt(45, new PointF(x, y));
                    break;

                case "Bottom Right":
                    m.RotateAt(135, new PointF(x, y));
                    break;

            }

            g.Transform = m;

            if (towerValue > 800)
            {
                g.ResetTransform();
                return;
            }
            if (towerValue == 50)
            {
                i_50++;
                g.DrawString("     " + Convert.ToString(towerValue), f, new SolidBrush(Color.White), x, y);
                if (i_50 > 1)
                {
                    towerValue += 50;
                }

            }
            else
            {
                g.DrawString("     " + Convert.ToString(towerValue), f, new SolidBrush(Color.White), x, y);
                towerValue += 50;

            }
            g.ResetTransform();



        }

    }
}
