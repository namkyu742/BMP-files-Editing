using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;         // Sleep


namespace SW3_ver_0._1
{
    class My_Bitmap
    {
        // bfHeader
        public char bfType1;
        public char bfType2;
        public int bfSize;
        public short bfReserved1;
        public short bfReserved2;
        public int bfOffset;

        // biHeader
        public int biSize;              // 현재 구조체의 크기
        public int biWidth;             // 비트맵 이미지의 가로 크기
        public int biHeight;            // 비트맵 이미지의 세로 크기
        public short biPlanes;          // 사용하는 색상판의 수
        public short biBitCount;        // 픽셀 하나를 표현하는 비트 수
        public int biCompression;       // 압축 방식
        public int biSizeImage;         // 비트맵 이미지의 픽셀 데이터 크기
        public int biXPelsPerMeter;     // 그림의 가로 해상도(미터당 픽셀)
        public int biYPelsPerMeter;     // 그림의 세로 해상도(미터당 픽셀)
        public int biClrUsed;           // 색상 테이블에서 실제 사용되는 색상 수
        public int biClrImportant;      // 비트맵을 표현하기 위해 필요한 색상 인덱스 수

        // pixels
        public struct RGBTRIPLE           // 24비트 비트맵 이미지의 픽셀 구조체
        {
            public byte rgbtBlue;          // 파랑
            public byte rgbtGreen;         // 초록
            public byte rgbtRed;           // 빨강
        }
        public RGBTRIPLE[] rgb;

        public struct RGBQUAD
        {
            public byte rgbtBlue;          // 파랑
            public byte rgbtGreen;         // 초록
            public byte rgbtRed;           // 빨강
            public byte reserved;           // 예약
            public Color color;
        }

        public void FileRead_BitmapFileHeader(My_Bitmap curbitmap, StreamReader sr)
        {
            char[] temp = new char[4];
            int sum = 0;
            curbitmap.bfType1 = (char)sr.Read();
            curbitmap.bfType2 = (char)sr.Read();

            sr.Read(temp, 0, 4);
            curbitmap.bfSize = 0;
            curbitmap.bfSize += temp[0] % 16;
            curbitmap.bfSize += temp[0] / 16 * 16;
            curbitmap.bfSize += temp[1] % 16 * 16 * 16;
            curbitmap.bfSize += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.bfSize += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.bfSize += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.bfSize += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.bfSize += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            sr.Read(temp, 0, 2);
            
            sum += temp[0] % 16;
            sum += temp[0] / 16 * 16;
            sum += temp[1] % 16 * 16 * 16;
            sum += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.bfReserved1 = (short)sum;

            sr.Read(temp, 0, 2);
            sum = 0;
            sum += temp[0] % 16;
            sum += temp[0] / 16 * 16;
            sum += temp[1] % 16 * 16 * 16;
            sum += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.bfReserved2 = (short)sum;

            sr.Read(temp, 0, 4);
            curbitmap.bfOffset = 0;
            curbitmap.bfOffset += temp[0] % 16;
            curbitmap.bfOffset += temp[0] / 16 * 16;
            curbitmap.bfOffset += temp[1] % 16 * 16 * 16;
            curbitmap.bfOffset += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.bfOffset += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.bfOffset += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.bfOffset += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.bfOffset += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;
        }
        public void FileRead_BitmapInfoHeader(My_Bitmap curbitmap, StreamReader sr)
        {
            char[] temp = new char[4];
            int sum = 0;
            // 비트맵 정보 헤더
            // biSize
            sr.Read(temp, 0, 4);
            curbitmap.biSize = 0;
            curbitmap.biSize += temp[0] % 16;
            curbitmap.biSize += temp[0] / 16 * 16;
            curbitmap.biSize += temp[1] % 16 * 16 * 16;
            curbitmap.biSize += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biSize += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biSize += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biSize += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biSize += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            // biWidth
            sr.Read(temp, 0, 4);
            curbitmap.biWidth = 0;
            curbitmap.biWidth += temp[0] % 16;
            curbitmap.biWidth += temp[0] / 16 * 16;
            curbitmap.biWidth += temp[1] % 16 * 16 * 16;
            curbitmap.biWidth += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biWidth += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biWidth += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biWidth += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biWidth += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;
           
            // biHeight
            sr.Read(temp, 0, 4);
            curbitmap.biHeight = 0;
            curbitmap.biHeight += temp[0] % 16;
            curbitmap.biHeight += temp[0] / 16 * 16;
            curbitmap.biHeight += temp[1] % 16 * 16 * 16;
            curbitmap.biHeight += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biHeight += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biHeight += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biHeight += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biHeight += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            // biPlanes
            sr.Read(temp, 0, 2);
            sum = 0;
            sum += temp[0] % 16;
            sum += temp[0] / 16 * 16;
            sum += temp[1] % 16 * 16 * 16;
            sum += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biPlanes = (short)sum;

            // biBitCount
            sr.Read(temp, 0, 2);
            sum = 0;
            sum += temp[0] % 16;
            sum += temp[0] / 16 * 16;
            sum += temp[1] % 16 * 16 * 16;
            sum += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biBitCount = (short)sum;

            // biCompression
            sr.Read(temp, 0, 4);
            curbitmap.biCompression = 0;
            curbitmap.biCompression += temp[0] % 16;
            curbitmap.biCompression += temp[0] / 16 * 16;
            curbitmap.biCompression += temp[1] % 16 * 16 * 16;
            curbitmap.biCompression += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biCompression += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biCompression += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biCompression += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biCompression += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            // biSizeImage
            sr.Read(temp, 0, 4);
            curbitmap.biSizeImage = 0;
            curbitmap.biSizeImage += temp[0] % 16;
            curbitmap.biSizeImage += temp[0] / 16 * 16;
            curbitmap.biSizeImage += temp[1] % 16 * 16 * 16;
            curbitmap.biSizeImage += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biSizeImage += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biSizeImage += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biSizeImage += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biSizeImage += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            // biXPelsPerMeter
            sr.Read(temp, 0, 4);
            curbitmap.biXPelsPerMeter = 0;
            curbitmap.biXPelsPerMeter += temp[0] % 16;
            curbitmap.biXPelsPerMeter += temp[0] / 16 * 16;
            curbitmap.biXPelsPerMeter += temp[1] % 16 * 16 * 16;
            curbitmap.biXPelsPerMeter += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biXPelsPerMeter += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biXPelsPerMeter += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biXPelsPerMeter += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biXPelsPerMeter += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            // biYPelsPerMeter
            sr.Read(temp, 0, 4);
            curbitmap.biYPelsPerMeter = 0;
            curbitmap.biYPelsPerMeter += temp[0] % 16;
            curbitmap.biYPelsPerMeter += temp[0] / 16 * 16;
            curbitmap.biYPelsPerMeter += temp[1] % 16 * 16 * 16;
            curbitmap.biYPelsPerMeter += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biYPelsPerMeter += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biYPelsPerMeter += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biYPelsPerMeter += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biYPelsPerMeter += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            // biClrUsed
            sr.Read(temp, 0, 4);
            curbitmap.biClrUsed = 0;
            curbitmap.biClrUsed += temp[0] % 16;
            curbitmap.biClrUsed += temp[0] / 16 * 16;
            curbitmap.biClrUsed += temp[1] % 16 * 16 * 16;
            curbitmap.biClrUsed += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biClrUsed += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biClrUsed += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biClrUsed += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biClrUsed += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;

            // biClrImportant
            sr.Read(temp, 0, 4);
            curbitmap.biClrImportant = 0;
            curbitmap.biClrImportant += temp[0] % 16;
            curbitmap.biClrImportant += temp[0] / 16 * 16;
            curbitmap.biClrImportant += temp[1] % 16 * 16 * 16;
            curbitmap.biClrImportant += temp[1] / 16 * 16 * 16 * 16;
            curbitmap.biClrImportant += temp[2] % 16 * 16 * 16 * 16 * 16;
            curbitmap.biClrImportant += temp[2] / 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biClrImportant += temp[3] % 16 * 16 * 16 * 16 * 16 * 16 * 16;
            curbitmap.biClrImportant += temp[3] / 16 * 16 * 16 * 16 * 16 * 16 * 16 * 16;
        }
        public void FileRead_BitmapPixels(My_Bitmap curbitmap, StreamReader sr, PictureBox pictureBox, TextBox tb1)
        {
            Bitmap temp3;
            curbitmap.rgb = new My_Bitmap.RGBTRIPLE[curbitmap.biHeight* curbitmap.biWidth];
            
            if (curbitmap.biBitCount == 24)      // 24비트 비트맵 파일
            {
                temp3 = new Bitmap(curbitmap.biWidth, curbitmap.biHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                char[] temp = new char[curbitmap.biWidth * curbitmap.biHeight];
                for (int i = 0; i < curbitmap.biHeight; i++)
                {
                    for (int j = 0; j < curbitmap.biWidth; j++)
                    {
                        //byte B = curbitmap.rgb[i + j * curbitmap.biHeight].rgbtBlue = (byte)(sr.Read());
                        //byte G = curbitmap.rgb[i + j * curbitmap.biHeight].rgbtGreen = (byte)(sr.Read());
                        //byte R = curbitmap.rgb[i + j * curbitmap.biHeight].rgbtRed = (byte)(sr.Read());

                        //temp3.SetPixel(j, curbitmap.biHeight - i - 1, Color.FromArgb(R, G, B));
                        //int B = sr.Read();
                        //int G = sr.Read();
                        //int R = sr.Read();
                        byte B = (byte)sr.Read();
                        byte G = (byte)sr.Read();
                        byte R = (byte)sr.Read();


                        temp3.SetPixel(j, i, Color.FromArgb(R, G, B));
                    }
                }
                pictureBox.Image = temp3;
            }
            else if (curbitmap.biBitCount == 8)     // 8비트 비트맵 파일
            {
                //temp3 = new Bitmap(curbitmap.biWidth, curbitmap.biHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                temp3 = new Bitmap(curbitmap.biWidth, curbitmap.biHeight);
                RGBQUAD[] table = new RGBQUAD[256];

                for (int i = 0; i < 256; i++)
                {
                    byte B = table[i].rgbtBlue = (byte)(sr.Read() * 4.047619047619047619047619047619);
                    byte G = table[i].rgbtGreen = (byte)(sr.Read() * 4.047619047619047619047619047619);
                    byte R = table[i].rgbtRed = (byte)(sr.Read() * 4.047619047619047619047619047619);
                    table[i].reserved = (byte)sr.Read();
                    table[i].color = Color.FromArgb(R, G, B);
                }

                for (int i = 0; i < curbitmap.biHeight; i++)
                {
                    for (int j = 0; j < curbitmap.biWidth; j++)
                    {
                        int p = sr.Read();

                        temp3.SetPixel(j, i, table[p].color);
                    }
                    pictureBox.Image = temp3;
                }
                pictureBox.Image = temp3;
            }
            else if (curbitmap.biBitCount == 4)     // 4비트 비트맵 파일
            {
                // 입력 영상에서 제외
            }
            else if (curbitmap.biBitCount == 1)
            {
                // 입력 영상에서 제외
            }
            else
            {
                MessageBox.Show("비트수준 (" + curbitmap.biBitCount + ") : 처리 가능한 파일이 아닙니다.");
                return;
            }
        }
    }
}
