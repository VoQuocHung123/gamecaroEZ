using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro_VoQuocHung
{
    public partial class frmQuocHung : Form
    {
        public frmQuocHung()
        {
            InitializeComponent();
            undoPlay1.Enabled = false;
            undoPlay2.Enabled = false;
        }
        int chess_W = 40;
        int chess_H = 40;
        
        static Boolean checkXO = true;
        public static string Check_XO()
        {

            if (checkXO == true)
            {

                checkXO = false;
                return "X";
            }
            checkXO = true;
            return "O";
        }

    
        private void btn_start_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            lbl_time1.Text = "00:00";
            lbl_time2.Text = "00:00";
            VeBanCo();
            timer1.Start();

        }
        public List<List<Button>> Matrix;
        public void VeBanCo()
        {
            pnlbanco.Enabled = true;
            pnlbanco.Controls.Clear();
            Matrix = new List<List<Button>>();
            Button btn1 = new Button() { Width = 0, Location = new Point(0) };
            for (int i = 0; i < 16; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j <= 16; j++)
                {
                    Button btn = new Button()
                    {
                        Width = chess_W,
                        Height = chess_H,
                        Location = new Point(btn1.Location.X + btn1.Width, btn1.Location.Y),
                        Tag = i.ToString()


                    };
                    pnlbanco.Controls.Add(btn);
                    Matrix[i].Add(btn);
                    btn1 = btn;
                    btn.Click += Btn_Click;
                   
                }
                btn1.Location = new Point(0, btn1.Location.Y + chess_H);
                btn1.Width = 0;
                btn1.Height = 0;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            
            Cls_QuocHung.playerX = 60;
            Cls_QuocHung.playerO = 60;
            Button btn = sender as Button;
            if(btn.Text != "")
            {
                return;
            }
            
            kiemtra();
            btn.Text = Check_XO();
            
             
            if (btn.Text == "X")
            {
                undoPlay1.Enabled = true;
                Cls_QuocHung.undoPlay1.Clear();
                Cls_QuocHung.undoPlay1.Push(btn);
                timer1.Stop();
                
                lbl_time1.Text = "00:00";
                
                
                if (Cls_QuocHung.checkO) timer2.Start();
                else
                {
                    undoPlay2.Enabled = true;
                    Cls_QuocHung.checkO = true;
                    timer2.Stop();
                    lbl_time2.Text = "00:00";
                    timer1.Start();
                    if (checkXO == false)
                    {
                        timer2.Stop();
                        lbl_time2.Text = "";
                        timer1.Start();
                    }

                }
               
               

            }
            else
            {
                Cls_QuocHung.undoPlay2.Clear();
                Cls_QuocHung.undoPlay2.Push(btn);
                
                timer2.Stop();
                lbl_time2.Text = "00:00";
                timer1.Start();

                if (Cls_QuocHung.checkX) timer1.Start();

                else
                {
                    Cls_QuocHung.checkX = true;
                    timer1.Stop();
                    lbl_time1.Text = "00:00";
                    timer2.Start();
                    if(checkXO == true)
                    {
                        timer2.Stop();
                        lbl_time2.Text = "";
                        timer1.Start();
                    }
                }
 
            }
            if (EndGame(btn))
            {
                Ketthuc(btn);
            }
            
        }

        public void xuLyHightLight(Button btn)
        {
            Cls_QuocHung.hightLight.Clear();
            Xulyhangngang(btn);
            Xulyhangdoc(btn);
            Xulycheochinh(btn);
            Xulycheophu(btn);
            while (Cls_QuocHung.hightLight.Count > 0)
            {
                Button b = Cls_QuocHung.hightLight.Pop();
                for (int i = 0; i < 16; i++)
                {

                    for (int j = 0; j <= 16; j++)
                    {
                       
                        if(Matrix[i][j] == b)
                        {
                            Matrix[i][j].BackColor = Color.Green;
                        }
                    }
                }

            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            pnlbanco.Controls.Clear();
            VeBanCo();
            lbl_time1.Text = "00:00";
            lbl_time2.Text = "00:00";
            lbl_round1.Text = "0";
            lbl_round2.Text = "0";
            lbl_win1.Text = "0";
            lbl_win2.Text = "0";
            timer1.Stop();
            timer2.Stop();
        }
        public bool EndGame(Button btn)
        {
            
            return Xulyhangdoc(btn) || Xulyhangngang(btn) || Xulycheochinh(btn) || Xulycheophu(btn);
        }
        public Point Laytoado(Button btn)
        {
            
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);
            return point;
        }
        public bool Xulyhangngang(Button btn)
        {
            Point point = Laytoado(btn);
            int dem = 0;
            for (int i = point.X; i >=0 ; i--)
            {
                if (Matrix[point.Y][i].Text == btn.Text)
                {
                    Cls_QuocHung.hightLight.Push(Matrix[point.Y][i]);
                    dem++;
                }
                else break;
            }
            
            for (int i = point.X+1; i < chess_W; i++)
            {
                if (Matrix[point.Y][i].Text == btn.Text)
                {
                    Cls_QuocHung.hightLight.Push(Matrix[point.Y][i]);
                    dem++;
                }
                else break;
            }
           
            
            int count = dem;
            if(count < 5)
            {
                while(count > 0)
                {
                    if (Cls_QuocHung.hightLight.Count > 0) Cls_QuocHung.hightLight.Pop();
                    count -= 1;
                }
            }
                return dem>=5;
        }
        public bool Xulyhangdoc(Button btn)
        {
            Point point = Laytoado(btn);
            int dem = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].Text == btn.Text)
                {
                    Cls_QuocHung.hightLight.Push(Matrix[i][point.X]);
                    dem++;
                }
                else break;
            }
            
            for (int i = point.Y + 1; i < chess_H; i++)
            {
                if (Matrix[i][point.X].Text == btn.Text)
                {
                    Cls_QuocHung.hightLight.Push(Matrix[i][point.X]);
                    dem++;
                }
                else break;
            }
            int count = dem;
            if (count < 5)
            {
                while (count > 0)
                {
                    if (Cls_QuocHung.hightLight.Count > 0) Cls_QuocHung.hightLight.Pop();
                    count -= 1;
                }
            }

            return dem >= 5;
        }
        
        public bool Xulycheophu(Button btn)
        {

            Point point = Laytoado(btn);
            int dem = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > chess_W || point.Y - i < 0) break;
                if (Matrix[point.Y - i][point.X + i].Text == btn.Text)
                {
                    Cls_QuocHung.hightLight.Push(Matrix[point.Y - i][point.X + i]);
                    dem++;
                }
                else break;
            }
            
            for (int i = 1; i <= chess_W - point.X; i++)
            {
                if (point.Y + i >= chess_H || point.X - i < 0) break;
                if (Matrix[point.Y + i][point.X - i].Text == btn.Text)
                {
                    Cls_QuocHung.hightLight.Push(Matrix[point.Y + i][point.X - i]);
                    dem++;
                }
                else break;
            }
            int count = dem;
            if (count < 5)
            {
                while (count > 0)
                {
                    if (Cls_QuocHung.hightLight.Count > 0) Cls_QuocHung.hightLight.Pop();
                    count -= 1;
                }
            }


            return dem >= 5;
        }
        public bool Xulycheochinh(Button btn)
        {
            Point point = Laytoado(btn);
            int dem = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0) break;
                if (Matrix[point.Y - i][point.X - i].Text == btn.Text)
                {
                    Cls_QuocHung.hightLight.Push(Matrix[point.Y - i][point.X - i]);
                    dem++;
                }
                else break;
            }
            
            for (int i = 1; i <= chess_W - point.X; i++)
            {
                if (point.Y + i >= chess_H || point.X + i >= chess_W) break;
                if (Matrix[point.Y + i][point.X + i].Text == btn.Text)
                {

                    Cls_QuocHung.hightLight.Push(Matrix[point.Y + i][point.X + i]);
                    dem++;
                }
                else break;
            }
            int count = dem;
            if (count < 5)
            {
                while (count > 0)
                {
                    if (Cls_QuocHung.hightLight.Count > 0) Cls_QuocHung.hightLight.Pop();
                    count -= 1;
                }
            }

            return dem >= 5;
        }
        int vong = 0;
        int demwin1 = 0;
        int demwin2 = 0;    
        public void Ketthuc(Button btn)
        {
            xuLyHightLight(btn);
           
            if (btn.Text == "X")
            {
                MessageBox.Show("Bên X thắng");
                demwin1 += 1;
                vong += 1;
                lbl_win1.Text = demwin1.ToString();
                lbl_round1.Text = vong.ToString();
                lbl_round2.Text = vong.ToString();
               
               
                pnlbanco.Enabled = false;
            }
            else { 
            
                MessageBox.Show("Bên O thắng");
                demwin2 += 1;
                vong += 1;
                lbl_win2.Text = demwin2.ToString();
                
                lbl_round2.Text = vong.ToString();
                lbl_round1.Text = vong.ToString();
                
                pnlbanco.Enabled = false;
            }
            
        }
        int t1 = 0;
        int t2 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            lbl_time2.Text = "00:00";
            timer1.Start();
            if (Cls_QuocHung.playerX > 0)
            {
                lbl_time1.Text = Cls_QuocHung.playerX.ToString();
                Cls_QuocHung.playerX -= 1;
                
            }
            else
            {
                MessageBox.Show("You Lose X");
            }
            t1++;
            //lbl_time1.Text = t1.ToString();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            t2++;
            timer1.Stop();
            timer2.Start();
            lbl_time1.Text = "00:00";
            if (Cls_QuocHung.playerO > 0)
            {
                lbl_time2.Text = Cls_QuocHung.playerO.ToString();
                Cls_QuocHung.playerO -= 1;
                
            }
            else
            {
                MessageBox.Show("You Lose O");
            }
        }

        private void kiemtra()
        {
            int countX = 0;
            int countO = 0;

            for (int i = 0; i < 16; i++)
            {

                for (int j = 0; j <= 16; j++)
                {
                    if (Matrix[i][j].Text =="X")
                    {
                        countX += 1;
                    }else if(Matrix[i][j].Text == "O")
                    {
                        countO += 1;
                    }    
                }
            }
            if(countX > countO)
            {
                checkXO = false;
               
            }
            
            else
            {
               
                checkXO = true;
            }
            
        }
        private void undoPlay1_Click(object sender, EventArgs e)
        {
                undoPlay1.Enabled = true;
                timer1.Start();
                Cls_QuocHung.checkO = false;
                timer2.Stop();
                lbl_time2.Text = "00:00";
                Button b1 = new Button();
                if (Cls_QuocHung.undoPlay1.Count > 0) b1 = Cls_QuocHung.undoPlay1.Pop();
                for (int i = 0; i < 16; i++)
                {

                    for (int j = 0; j <= 16; j++)
                    {

                        if (Matrix[i][j] == b1)
                        {
                            Matrix[i][j].Text = "";
                            break;
                        }
                    }
                }
            
          
            
        }

        private void undoPlay2_Click(object sender, EventArgs e)
        {
            undoPlay2.Enabled = true;
            timer2.Start();
            Cls_QuocHung.checkX = false;
            timer1.Stop();
            lbl_time1.Text= "00:00";
            Button b1 = new Button();
            if (Cls_QuocHung.undoPlay2.Count > 0) b1 = Cls_QuocHung.undoPlay2.Pop();
            for (int i = 0; i < 16; i++)
            {

                for (int j = 0; j <= 16; j++)
                {

                    if (Matrix[i][j] == b1)
                    {
                        Matrix[i][j].Text = "";
                        break;
                    }
                }
            }
        }
    }
}
