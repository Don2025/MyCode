using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()    //默认构造函数
        {
            InitializeComponent();
        }

        bool isEqual = false;   //判断一次计算是否完成

        public void Initialize()   //初始化
        {
            textBox1.Text = "";
            textBox2.Text = "";
            isEqual = false;
        }

        private void Button0_Click(object sender, EventArgs e)
        {
            if(isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "0";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "1";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "2";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "3";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "4";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "5";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "6";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "7";
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "8";
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += "9";
        }

        private void ButtonDot_Click(object sender, EventArgs e)
        {
            if (isEqual)
            {
                Initialize();
                isEqual = false;
            }
            textBox1.Text += ".";
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if(isEqual)
            {
                textBox1.Text = textBox2.Text;
                textBox2.Text = "";
                isEqual = false;
            }
            textBox1.Text += "+";
        }

        private void ButtonSub_Click(object sender, EventArgs e)
        {
            if(isEqual)
            {
                textBox1.Text = textBox2.Text;
                textBox2.Text = "";
                isEqual = false;
            }
            textBox1.Text += "-";
        }

        private void ButtonMul_Click(object sender, EventArgs e)
        {
            if(isEqual)
            {
                textBox1.Text = textBox2.Text;
                textBox2.Text = "";
                isEqual = false;
            }
            textBox1.Text += "×";
        }

        private void ButtonDiv_Click(object sender, EventArgs e)
        {
            if(isEqual)
            {
                textBox1.Text = textBox2.Text;
                textBox2.Text = ""; 
                isEqual = false;
            }
            textBox1.Text += "÷";
        }

        private void ButtonAC_Click(object sender, EventArgs e)   //全部清零
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void ButtonBack_Click(object sender, EventArgs e)   //用于删除textBox的最后一个字符
        {
            if (textBox1.Text.Length != 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
        }

        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text.Replace('÷', '/').Replace('×', '*');  //替换一下乘号和除号
            var sum = new DataTable().Compute(str, null);
            textBox2.Text = "";
            textBox2.Text += sum;
            if(textBox2.Text == "∞")
            {
                textBox2.Text = "除数不能为0!";
            }
            isEqual = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.D0: case Keys.NumPad0: Button0_Click(sender, e); break;
                case Keys.D1: case Keys.NumPad1: Button1_Click(sender, e); break;
                case Keys.D2: case Keys.NumPad2: Button2_Click(sender, e); break;
                case Keys.D3: case Keys.NumPad3: Button3_Click(sender, e); break;
                case Keys.D4: case Keys.NumPad4: Button4_Click(sender, e); break;
                case Keys.D5: case Keys.NumPad5: Button5_Click(sender, e); break;
                case Keys.D6: case Keys.NumPad6: Button6_Click(sender, e); break;
                case Keys.D7: case Keys.NumPad7: Button7_Click(sender, e); break;
                case Keys.D8: case Keys.NumPad8: Button8_Click(sender, e); break;
                case Keys.D9: case Keys.NumPad9: Button9_Click(sender, e); break;
                case Keys.Decimal: ButtonDot_Click(sender, e); break;
                case Keys.Add: ButtonAdd_Click(sender, e); break;
                case Keys.Subtract: ButtonSub_Click(sender, e); break;
                case Keys.Multiply: ButtonMul_Click(sender, e); break;
                case Keys.Divide: ButtonDiv_Click(sender, e); break;
                case Keys.Back: ButtonBack_Click(sender, e); break;
                case Keys.Oemplus: ButtonEquals_Click(sender, e); break;
                default: break;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();   //设置label2的文本为当前的系统时间
        }

    }
}
