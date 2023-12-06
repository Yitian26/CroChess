using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace crochess
{
    public partial class Form2 : Form
    {
        string first;
        string second;
        int mode;
        int nowplayer;
        int totalstep;
        Color fillcolor;
        int[] map;
        int[] map1;
        int[][] reflect = new int[9][];
        public Form2(string f,string s,int m)
        {
            InitializeComponent();
            first = f;
            second = s;
            mode = m;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            initialize();
            if (first == "电脑")
            {
                RobotOnPlay();
            }
        }

        private void initialize()
        {
            richTextBox1.AppendText("对局开始\n");
            richTextBox1.AppendText(first + "先手落子\n");
            nowplayer = 1;
            totalstep = 0;
            fillcolor = Color.Black;
            reflect[0] = new int[2] { 0, 0 };
            reflect[1] = new int[2] { 0, 1 };
            reflect[2] = new int[2] { 0, 2 };
            reflect[3] = new int[2] { 1, 0 };
            reflect[4] = new int[2] { 1, 1 };
            reflect[5] = new int[2] { 1, 2 };
            reflect[6] = new int[2] { 2, 0 };
            reflect[7] = new int[2] { 2, 1 };
            reflect[8] = new int[2] { 2, 2 };
            map = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            map1 = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            button1.BackColor = Color.LightGray;
            button2.BackColor = Color.LightGray;
            button3.BackColor = Color.LightGray;
            button4.BackColor = Color.LightGray;
            button5.BackColor = Color.LightGray;
            button6.BackColor = Color.LightGray;
            button7.BackColor = Color.LightGray;
            button8.BackColor = Color.LightGray;
            button9.BackColor = Color.LightGray;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }
        private void CallNextRound(int[] step)
        {
            totalstep++;
            int nowbutton = step[0] * 3 + step[1];
            map1[nowbutton] = nowplayer;
            updatemap(nowbutton);
            if (nowplayer == 1)
            {
                richTextBox1.AppendText(first + "在(" + Convert.ToString(step[0]) + "," + Convert.ToString(step[1]) + ")落子\n");
                nowplayer = 4;
                fillcolor = Color.White;
            }
            else
            {
                richTextBox1.AppendText(second + "在(" + Convert.ToString(step[0]) + "," + Convert.ToString(step[1]) + ")落子\n");
                nowplayer = 1;
                fillcolor = Color.Black;
            }
            if (mode == 1)
            {
                RobotOnPlay();
            }
            if (Judge())
            {
                if (nowplayer == 1)
                { richTextBox1.AppendText(first + "正在思考\n"); }
                else
                { richTextBox1.AppendText(second + "正在思考\n"); }
            }
        }

        private void updatemap(int nowbutton)
        {
            switch (nowbutton)
            {
                case 0:
                    map[0] = map[0] + nowplayer;
                    map[3] = map[3] + nowplayer;
                    map[6] = map[6] + nowplayer;
                    button1.BackColor = fillcolor;
                    button1.Enabled = false;
                    break;
                case 1:
                    map[0] = map[0] + nowplayer;
                    map[4] = map[4] + nowplayer;
                    button2.BackColor = fillcolor;
                    button2.Enabled = false;
                    break;
                case 2:
                    map[0] = map[0] + nowplayer;
                    map[5] = map[5] + nowplayer;
                    map[7] = map[7] + nowplayer;
                    button3.BackColor = fillcolor;
                    button3.Enabled = false;
                    break;
                case 3:
                    map[1] = map[1] + nowplayer;
                    map[3] = map[3] + nowplayer;
                    button4.BackColor = fillcolor;
                    button4.Enabled = false;
                    break;
                case 4:
                    map[1] = map[1] + nowplayer;
                    map[4] = map[4] + nowplayer;
                    map[6] = map[6] + nowplayer;
                    map[7] = map[7] + nowplayer;
                    button5.BackColor = fillcolor;
                    button5.Enabled = false;
                    break;
                case 5:
                    map[1] = map[1] + nowplayer;
                    map[5] = map[5] + nowplayer;
                    button6.BackColor = fillcolor;
                    button6.Enabled = false;
                    break;
                case 6:
                    map[2] = map[2] + nowplayer;
                    map[3] = map[3] + nowplayer;
                    map[7] = map[7] + nowplayer;
                    button7.BackColor = fillcolor;
                    button7.Enabled = false;
                    break;
                case 7:
                    map[2] = map[2] + nowplayer;
                    map[4] = map[4] + nowplayer;
                    button8.BackColor = fillcolor;
                    button8.Enabled = false;
                    break;
                case 8:
                    map[2] = map[2] + nowplayer;
                    map[5] = map[5] + nowplayer;
                    map[6] = map[6] + nowplayer;
                    button9.BackColor = fillcolor;
                    button9.Enabled = false;
                    break;
            }
        }

        private bool Judge()
        {
            int result = checkwin();
            switch (result)
            {
                case 0:
                    lockall();
                    richTextBox1.AppendText("平局");
                    return false;
                case 1:
                    lockall();
                    richTextBox1.AppendText(first+"获胜");
                    return false;
                case 4:
                    lockall();
                    richTextBox1.AppendText(second+"获胜");
                    return false;
                default:
                    return true;
            }
        }

        private int checkwin()
        {
            for(int i = 0; i < 8; i++)
            {
                if (map[i] == 3) { return 1; }
                else if(map[i] == 12){ return 4; }
            }
            if (totalstep == 9) { return 0; }
            return 3;
        }

        private void lockall()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }

        private void RobotOnPlay()
        {
            richTextBox1.AppendText("电脑正在思考\n");
            Random rand = new Random();
            if (checkwin()==3)
            {
                int step = rand.Next(0, 9);
                while(map1[step] != 0) { step = rand.Next(0, 9); }
                totalstep++;
                map1[step] = nowplayer;
                updatemap(step);
                int y = step % 3;
                int x = (step-y)/3;
                if (nowplayer == 1)
                {
                    richTextBox1.AppendText(first + "在(" + Convert.ToString(x) + "," + Convert.ToString(y) + ")落子\n");
                    nowplayer = 4;
                    fillcolor = Color.White;
                }
                else
                {
                    richTextBox1.AppendText(second + "在(" + Convert.ToString(x) + "," + Convert.ToString(y) + ")落子\n");
                    nowplayer = 1;
                    fillcolor = Color.Black;
                }
                if (step == 0|step == 4|step==8)
                {
                    if (nowplayer == 1) 
                    { 
                        nowplayer = 4;
                        fillcolor= Color.White;
                    }
                    else
                    {
                        nowplayer = 1;
                        fillcolor = Color.Black;
                    }
                    Thread.Sleep(1000);
                    richTextBox1.AppendText("电脑打断了你的回合\n");
                    RobotOnPlay();
                }
            }
            else if (checkwin()!=nowplayer)
            {
                Thread.Sleep(1000);
                richTextBox1.Clear();
                richTextBox1.AppendText("电脑推翻了棋盘\n");
                initialize();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[1]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[2]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[3]);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[4]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[5]);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[6]);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[7]);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CallNextRound(reflect[8]);
        }
    }
}
