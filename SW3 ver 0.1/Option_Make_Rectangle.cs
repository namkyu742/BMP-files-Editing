using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW3_ver_0._1
{
    public partial class Option_Make_Rectangle : Form
    {
        public Main Indicator_M;
        int RGB_R, RGB_G, RGB_B;

        private bool bLeftButtonDown = false;               // 마우스 조작 확인
        private Point ClickPoint = new Point();             // 시작점
        private Point CurrentTopLeft = new Point();         // 시작점
        private Point CurrentBottomRight = new Point();     // 끝점
        private Pen MyPen;                                  // 펜 설정
        private Graphics g;                                 // 사각형 저장
        private Color temp_c;                               // 사각형 색상 임시 저장
        decimal O_sx, O_sy, O_fx, O_fy;                     // 임시 사각형에 비례한 원본 사각형의 좌표

        public Option_Make_Rectangle() { InitializeComponent(); }
        private void Option_Make_Rectangle_Load(object sender, EventArgs e)
        {
            // 초기 설정
            this.pictureBox2.Image = Indicator_M.pictureBox1.Image;
            this.Color_Palette.BackColor = Color.FromArgb(RGB_R, RGB_G, RGB_B);
            g = this.pictureBox2.CreateGraphics();
            MyPen = new Pen(Color.Red, 2);
            MyPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        }
        private void Button_Accept_Click(object sender, EventArgs e)
        {
            // 적용 버튼
            make_rectangle();
            this.Close();
        }
        private void Button_Cancle_Click(object sender, EventArgs e)
        {
            // 취소 버튼
            this.Close(); 
        }
        private void Color_Palette_Manager(object sender, EventArgs e)
        {
            // 색상표 사용
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Color_Palette.BackColor = this.colorDialog1.Color;
                this.numericUpDown5.Value = this.colorDialog1.Color.R;
                this.numericUpDown6.Value = this.colorDialog1.Color.G;
                this.numericUpDown7.Value = this.colorDialog1.Color.B;
            }
            else { }
        }
        private void Color_ValueChanged(object sender, EventArgs e)
        {
            // 색상값 변경 시
            RGB_R = (int)numericUpDown5.Value;
            RGB_G = (int)numericUpDown6.Value;
            RGB_B = (int)numericUpDown7.Value;
            temp_c = Color.FromArgb(RGB_R, RGB_G, RGB_B);
            this.Color_Palette.BackColor = temp_c;
        }

        // --------------------- 마우스로 사각형 좌표 구하기 --------------------- //
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            bLeftButtonDown = true;
            this.Cursor = Cursors.Cross;    // 커서 크로스로 변경
            ClickPoint = new Point(e.X, e.Y);
            this.numericUpDown1.Value = e.X;
            this.numericUpDown2.Value = e.Y;
            this.pictureBox2.Refresh();
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (bLeftButtonDown)
            {
                //X좌표 계산
                if (e.X < ClickPoint.X)
                {
                    CurrentTopLeft.X = e.X;
                    CurrentBottomRight.X = ClickPoint.X;
                }
                else
                {
                    CurrentTopLeft.X = ClickPoint.X;
                    CurrentBottomRight.X = e.X;
                }
                //Y좌표계산
                if (e.Y < ClickPoint.Y)
                {
                    CurrentTopLeft.Y = e.Y;
                    CurrentBottomRight.Y = ClickPoint.Y;
                }
                else
                {
                    CurrentTopLeft.Y = ClickPoint.Y;
                    CurrentBottomRight.Y = e.Y;
                }

                // 색상 테스트
                //Bitmap temp = new Bitmap(this.pictureBox2.Image);
                //Color clr = temp.GetPixel(e.X, e.Y);
                //this.numericUpDown5.Value = clr.R;
                //this.numericUpDown6.Value = clr.G;
                //this.numericUpDown7.Value = clr.B;

                pictureBox2.Refresh();  //화면초기화
                g.DrawRectangle(MyPen, CurrentTopLeft.X, CurrentTopLeft.Y, CurrentBottomRight.X - CurrentTopLeft.X, CurrentBottomRight.Y - CurrentTopLeft.Y);   //사각형 그리기
            }
        }
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            bLeftButtonDown = false;
            if (e.X < 0)
                this.numericUpDown3.Value = 0;
            else if (e.X > this.pictureBox2.Width)
                this.numericUpDown3.Value = this.pictureBox2.Width - 1;
            else
                this.numericUpDown3.Value = e.X;

            if (e.Y < 0)
                this.numericUpDown4.Value = 0;
            else if (e.Y > this.pictureBox2.Height)
                this.numericUpDown4.Value = this.pictureBox2.Height - 1;
            else
                this.numericUpDown4.Value = e.Y;

            // 좌표 정리(기본값 2사분면)
            if (this.numericUpDown1.Value > this.numericUpDown3.Value && this.numericUpDown2.Value > this.numericUpDown4.Value)
            {
                // 시작점이 4사분면에 있는 경우
                decimal tempValue = this.numericUpDown1.Value;
                this.numericUpDown1.Value = this.numericUpDown3.Value;
                this.numericUpDown3.Value = tempValue;
                tempValue = this.numericUpDown2.Value;
                this.numericUpDown2.Value = this.numericUpDown4.Value;
                this.numericUpDown4.Value = tempValue;
            }
            else if(this.numericUpDown1.Value < this.numericUpDown3.Value && this.numericUpDown2.Value > this.numericUpDown4.Value)
            {
                // 시작점이 3사분면에 있는 경우
                decimal tempValue = this.numericUpDown2.Value;
                this.numericUpDown2.Value = this.numericUpDown4.Value;
                this.numericUpDown4.Value = tempValue;
            }
            else if (this.numericUpDown1.Value > this.numericUpDown3.Value && this.numericUpDown2.Value < this.numericUpDown4.Value)
            {
                // 시작점이 1사분면에 있는 경우
                decimal tempValue = this.numericUpDown1.Value;
                this.numericUpDown1.Value = this.numericUpDown3.Value;
                this.numericUpDown3.Value = tempValue;
            }
            this.Cursor = Cursors.Default;  // 커서 되돌리기

            // 비례 좌표 계산
            double temp1 = (double)Indicator_M.pictureBox1.Width / this.pictureBox2.Width;
            double temp2 = (double)Indicator_M.pictureBox1.Height / this.pictureBox2.Height;

            O_sx = Decimal.ToInt32((decimal)(temp1 * (int)this.numericUpDown1.Value));
            O_sy = Decimal.ToInt32((decimal)(temp2 * (int)this.numericUpDown2.Value));
            O_fx = Decimal.ToInt32((decimal)(temp1 * (int)this.numericUpDown3.Value));
            O_fy = Decimal.ToInt32((decimal)(temp2 * (int)this.numericUpDown4.Value));
        }
        // -----------------------------------------------------------------------------------------//
        private void make_rectangle()
        {
            // 원본 이미지에 사각형 생성
            Bitmap temp = new Bitmap(Indicator_M.pictureBox1.Image);
            for (int i = 0; i < O_fx; i++)
                for (int j = 0; j < O_fy; j++) 
                    if (i > O_sx && j > O_sy) 
                        temp.SetPixel(i, j, temp_c);
            Indicator_M.pictureBox1.Image = temp;
        }
        private void Hot_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter, Esc 키 사용
            if (e.KeyChar == (char)Keys.Enter)
                Button_Accept_Click(sender, e);
            if (e.KeyChar == (char)Keys.Escape)
                Button_Cancle_Click(sender, e);
        }
    }
}
