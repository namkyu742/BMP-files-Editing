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
    public partial class Program_Info : Form
    {
        // 도움말 출력 대화창
        public Program_Info() { InitializeComponent(); }
        private void button_Accept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
