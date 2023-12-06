using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crochess
{
    public partial class Form1 : Form
    {
        public bool ongame = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string first;
            string second;
            Random random = new Random();
            if (random.Next(0, 2) == 1)
            {
                first = "玩家1";
                second = "玩家2";
            }
            else
            {
                first = "玩家2";
                second = "玩家1";
            }
            Form2 Battle = new Form2(first,second,0);
            Battle.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //当窗体最小化时
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                Hide();
                //将托盘图标设为显示
                Visible = true;
            }
            else
            {
                //将托盘图标设为隐藏
                Visible = false;
                ShowInTaskbar = true;
                //显示窗体
                Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string first;
            string second;
            Random random = new Random();
            if (random.Next(0, 2) == 1)
            {
                first = "玩家";
                second = "电脑";
            }
            else
            {
                first = "电脑";
                second = "玩家";
            }
            Form2 Battle = new Form2(first, second, 1);
            Battle.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("该功能尚未开放");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }
    }
}
