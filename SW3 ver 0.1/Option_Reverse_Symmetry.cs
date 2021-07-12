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
    public partial class Option_Reverse_Symmetry : Form
    {
        // 대칭 반전 처리
        public Main Indicator_M;
        public Option_Reverse_Symmetry() { InitializeComponent(); }
        private void Button_Accept_Click(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(Indicator_M.pictureBox1.Image);        // 원본
            Bitmap temp2 = new Bitmap(Indicator_M.pictureBox1.Image);       // 수정
            Color colorData;
            if (this.radioButton1.Checked)
            {
                for (int i = 0; i < temp.Width; i++)
                    for (int j = 0; j < temp.Height; j++)
                    {
                        colorData = temp.GetPixel(i, j);
                        temp2.SetPixel(i, temp.Height - j - 1, colorData);
                    }
            }
            else if (this.radioButton2.Checked)
            {
                for (int i = 0; i < temp.Width; i++)
                    for (int j = 0; j < temp.Height; j++)
                    {
                        colorData = temp.GetPixel(i, j);
                        temp2.SetPixel(temp.Width - i - 1, j, colorData);
                    }
            }
            else if (this.radioButton3.Checked)
            {
                for (int i = 0; i < temp.Width; i++)
                    for (int j = 0; j < temp.Height; j++)
                    {
                        colorData = temp.GetPixel(i, j);
                        temp2.SetPixel(temp.Width - i - 1, temp.Height - j - 1, colorData);
                    }
            }
            else {  } // 선택될 일이 없음
            Indicator_M.pictureBox1.Image = temp2;
            this.Close();
        }
        private void Button_Cancle_Click(object sender, EventArgs e)
        {
            // 대화창 닫기
            this.Close();
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
