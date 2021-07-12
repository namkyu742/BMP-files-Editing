using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

// 실행 파일 위치 : C:\Users\namky\OneDrive\바탕 화면\SW3 ver 0.3\SW3 ver 0.1\bin\Debug\SW3 ver 0.1.exe
//  this.textBox1.AppendText(Application.ExecutablePath);   // 실행파일 위치
// System.Environment.CurrentDirectory //실행파일 경로

namespace SW3_ver_0._1
{
    public partial class Main : Form
    {
        string open_path = "NO PATH";       // 현재 불러온 파일의 경로 (파일 이름 포함)
        string original_name;               // 현재 불러온 파일의 이름
        //string original_path = "C:\\Users\\namky\\OneDrive\\바탕 화면\\SW3 ver 0.5\\SW3 ver 0.1\\BMP 파일\\";
        string default_path = System.Environment.CurrentDirectory + "\\Image_file";
        int contrast = 0;
       
        My_Bitmap curbitmap;
        public Main() { InitializeComponent(); }
        private void Main_Load(object sender, EventArgs e)
        {
            // 프로그램 실행 시 1회만 실행
            this.toolStripStatusLabel1.Text = "";       // 상태바 초기화
            this.textBox2.Text = "명암대비 레벨 : " + contrast.ToString();
        }
        private void File_Open(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = default_path;
            openFileDialog1.Filter = "BMP 파일 (*.bmp)|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                open_path = openFileDialog1.FileName;
                // ----------------------- 파일 속성 표시 ----------------------- 
                var info = new FileInfo(open_path);
                this.original_name = Path.GetFileNameWithoutExtension(open_path);
                this.textBox1.Text = "";
                this.textBox1.AppendText("파일 이름    : " + Path.GetFileNameWithoutExtension(open_path) + "\r\n");
                this.textBox1.AppendText("파일 확장자 : " + Path.GetExtension(open_path) + "\r\n");
                this.textBox1.AppendText("파일 사이즈 : " + info.Length / 1024 + "KB\r\n");
                this.textBox1.AppendText("만든 날짜    : " + info.CreationTime + "\r\n");
                Bitmap open_bmp = new Bitmap(open_path);
                this.pictureBox1.Image = open_bmp; 

                // My_Bitmap
                StreamReader sr = new StreamReader(open_path, Encoding.UTF8);      
                // ASCII, UTF7 는 7비트 인코딩이라 127을 넘어서는 수의 표현이 불가능
                // UTF8, UNICODE...

                curbitmap = new My_Bitmap();         // My_Bitmap 생성

                // 비트맵 파일 헤더 읽기
                curbitmap.FileRead_BitmapFileHeader(curbitmap, sr);

                // 비트맵 정보 헤더 읽기
                curbitmap.FileRead_BitmapInfoHeader(curbitmap, sr);

                // 비트맵 픽셀 읽기
                //curbitmap.FileRead_BitmapPixels(curbitmap, sr, this.pictureBox2, this.textBox1);  

                // 확인 - 헤더 정보 텍스트 박스에 출력
                View_Header();
                sr.Dispose();
                sr.Close();
                contrast = 0;
                this.textBox2.Text = "명암대비 레벨 : " + contrast.ToString();
            }
        }
        private void File_Save(object sender, EventArgs e)
        {
            MessageBox.Show("구현 실패");
            //if (open_path.Equals("NO PATH"))
            //{
            //    MessageBox.Show("파일 지정 필요!");
            //    return;
            //}
            //string save_name = "NO_NAME";
            //pictureBox1.Image.Save(original_path + "temp777.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            //Bitmap bmp = new Bitmap(this.pictureBox1.Image);
            //this.pictureBox1.Image.Dispose();
            //this.pictureBox1.Dispose(); // 픽쳐박스 해제
            //System.IO.File.Delete(open_path);
           
        }
        private void File_Save_another_name(object sender, EventArgs e)
        {
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            string save_path = "NO PATH";
            saveFileDialog1.InitialDirectory = default_path;
            saveFileDialog1.Filter = "BMP 파일 (*.bmp)|*.bmp";    //|PPM 파일 (*.ppm)|*.ppm|PGM 파일 (*.pgm)|*.pgm";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                save_path = saveFileDialog1.FileName;
                pictureBox1.Image.Save(save_path + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }
        private void ShutDown(object sender, EventArgs e)
        {
            // 프로그램 종료
            Environment.Exit(0);
        }
        private void Program_Info(object sender, EventArgs e)
        {
            // 도움말 - 프로그램 정보
            Program_Info program_info = new Program_Info();
            program_info.ShowDialog();
        }

        // ----------------------------- Clicked Event ----------------------------- 
        private void Processing_Reverse_Clicked(object sender, EventArgs e)
        {
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            this.toolStripStatusLabel1.Text = "[밝기 반전]";
            Processing_Reverse(this.pictureBox1);
        }
        private void Processing_Symmetry_Clicked(object sender, EventArgs e)
        {
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            this.toolStripStatusLabel1.Text = "[대칭 반전]";
            Option_Reverse_Symmetry ORS = new Option_Reverse_Symmetry();
            ORS.Indicator_M = this;
            ORS.ShowDialog();
        }
        private void Processing_Make_Rectangle_Clicked(object sender, EventArgs e)
        {            
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            this.toolStripStatusLabel1.Text = "[사각형 생성]";
            Option_Make_Rectangle OMR = new Option_Make_Rectangle();
            OMR.Indicator_M = this;
            OMR.ShowDialog();
        }
        private void Processing_arithmetic_Clicked(object sender, EventArgs e)
        {
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            this.toolStripStatusLabel1.Text = "[영상 합산]";
            Processing_arithmetic(this.pictureBox1, this.pictureBox2);
        }
        private void Processing_Gray_Conversion_Clicked(object sender, EventArgs e)
        {
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            this.toolStripStatusLabel1.Text = "[그레이 스케일]";
            Processing_Gray_Conversion(this.pictureBox1);
        }
        private void Contrast_Increase(object sender, EventArgs e)
        {
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            this.toolStripStatusLabel1.Text = "[명암 대비 증가]";
            Bitmap temp = new Bitmap(this.pictureBox1.Image);
            Color colorData;
            for (int i = 0; i < temp.Width; i++)
                for (int j = 0; j < temp.Height; j++)
                {
                    colorData = temp.GetPixel(i, j);
                    int colorR = (int)(colorData.R * 1.2);
                    int colorG = (int)(colorData.G * 1.2);
                    int colorB = (int)(colorData.B * 1.2);
                    if (colorR > 255) colorR = 255;
                    if (colorG > 255) colorG = 255;
                    if (colorB > 255) colorB = 255;
                    colorData = Color.FromArgb(colorR, colorG, colorB);
                    temp.SetPixel(i, j, colorData);
                }
            this.pictureBox1.Image = temp;
            this.contrast++;
            this.textBox2.Text = "명암대비 레벨 : " + contrast.ToString();
        }
        private void Contrast_Decrease(object sender, EventArgs e)
        {
            if (open_path.Equals("NO PATH"))
            {
                MessageBox.Show("파일 지정 필요!");
                return;
            }
            this.toolStripStatusLabel1.Text = "[명암 대비 감소]";
            Bitmap temp = new Bitmap(this.pictureBox1.Image);
            Color colorData;
            for (int i = 0; i < temp.Width; i++)
                for (int j = 0; j < temp.Height; j++)
                {
                    colorData = temp.GetPixel(i, j);
                    int colorR = (int)((double)colorData.R / 1.2);
                    int colorG = (int)((double)colorData.G / 1.2);
                    int colorB = (int)((double)colorData.B / 1.2);
                    if (colorR < 0) colorR = 0;
                    if (colorG < 0) colorG = 0;
                    if (colorB < 0) colorB = 0;
                    colorData = Color.FromArgb(colorR, colorG, colorB);
                    temp.SetPixel(i, j, colorData);
                }
            this.pictureBox1.Image = temp;
            this.contrast--;
            this.textBox2.Text = "명암대비 레벨 : " + contrast.ToString();
        }

        // ----------------------------- function ----------------------------- 
        private void Processing_Reverse(PictureBox pictureBox)
        {
            Bitmap temp = new Bitmap(pictureBox.Image);
            Color colorData;
            for (int i = 0; i < temp.Width; i++)
                for (int j = 0; j < temp.Height; j++)
                {
                    colorData = temp.GetPixel(i, j);
                    int colorR = colorData.R ^ 255;
                    int colorG = colorData.G ^ 255;
                    int colorB = colorData.B ^ 255;
                    colorData = Color.FromArgb(colorR, colorG, colorB);
                    temp.SetPixel(i, j, colorData);
                }
            pictureBox.Image = temp;
        }
        private void Processing_arithmetic(PictureBox pictureBox1, PictureBox pictureBox2)
        {
            // 영상합산 실험 - 아직 구현중
            string path;
            openFileDialog1.InitialDirectory = default_path;
            openFileDialog1.Filter = "BMP 파일 (*.bmp)|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                pictureBox2.Load(path);   // 화면 표시
                Bitmap temp = new Bitmap(pictureBox1.Image);
                Bitmap temp2 = new Bitmap(pictureBox2.Image);

                for (int i = 0; i < temp2.Width; i++)
                    for (int j = 0; j < temp2.Height; j++)
                    {
                        Color colorData1 = temp.GetPixel(i, j);
                        Color colorData2 = temp2.GetPixel(i, j);
                        int R = colorData1.R + colorData2.R;
                        int G = colorData1.G + colorData2.G;
                        int B = colorData1.B + colorData2.B;
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;

                        temp2.SetPixel(i, j, Color.FromArgb(R, G, B));
                    }
                pictureBox1.Image = temp2;
            }
        }
        private void Processing_Gray_Conversion(PictureBox pictureBox)
        {
            Bitmap temp = new Bitmap(pictureBox.Image);
            Color colorData;
            for (int i = 0; i < temp.Width; i++)
                for (int j = 0; j < temp.Height; j++)
                {
                    colorData = temp.GetPixel(i, j);
                    int colorR = (colorData.R + colorData.G + colorData.B) / 3;
                    int colorG = (colorData.R + colorData.G + colorData.B) / 3;
                    int colorB = (colorData.R + colorData.G + colorData.B) / 3;
                    colorData = Color.FromArgb(colorR, colorG, colorB);
                    temp.SetPixel(i, j, colorData);
                }
            pictureBox.Image = temp;
        }
        private void View_Header()
        {
            this.textBox1.AppendText("\r\n-----------------------------------\r\n");
            this.textBox1.AppendText("< 비트맵 파일 헤더 >\r\n");
            this.textBox1.AppendText("bfType : " + curbitmap.bfType1.ToString() + curbitmap.bfType2.ToString() + "\r\n");
            this.textBox1.AppendText("bfSize : " + curbitmap.bfSize + "(" + curbitmap.bfSize / 1024 + "KB)" + "\r\n");
            this.textBox1.AppendText("bfReserved1 : " + curbitmap.bfReserved1 + "\r\n");
            this.textBox1.AppendText("bfReserved2 : " + curbitmap.bfReserved2 + "\r\n");
            this.textBox1.AppendText("bfOffset : " + curbitmap.bfOffset + "\r\n\r\n");
            this.textBox1.AppendText("< 비트맵 정보 헤더 >\r\n");
            this.textBox1.AppendText("biSize : " + curbitmap.biSize + "\r\n");
            this.textBox1.AppendText("biWidth : " + curbitmap.biWidth + "\r\n");
            this.textBox1.AppendText("biHeight : " + curbitmap.biHeight + "\r\n");
            this.textBox1.AppendText("biPlanes : " + curbitmap.biPlanes + "\r\n");
            this.textBox1.AppendText("biBitCount : " + curbitmap.biBitCount + "\r\n");
            this.textBox1.AppendText("biCompression : " + curbitmap.biCompression + "\r\n");
            this.textBox1.AppendText("biSizeImage : " + curbitmap.biSizeImage + "\r\n");
            this.textBox1.AppendText("biXPelsPerMeter : " + curbitmap.biYPelsPerMeter + "\r\n");
            this.textBox1.AppendText("biYPelsPerMeter : " + curbitmap.biYPelsPerMeter + "\r\n");
            this.textBox1.AppendText("biClrUsed : " + curbitmap.biClrUsed + "\r\n");
            this.textBox1.AppendText("biClrImportant : " + curbitmap.biClrImportant + "\r\n\r\n");
        }
    }
}