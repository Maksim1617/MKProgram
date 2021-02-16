using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyclicCodes
{
    public partial class Form2 : Form
    {
        int count = 1;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            count = 1;
            this.BackgroundImage = Properties.Resources._1;
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            count--;
            if (count < 1)
            {
                count = 15;
            }
            if (count == 1)
            {
                this.BackgroundImage = Properties.Resources._1;
            }
            if (count == 2)
            {
                this.BackgroundImage = Properties.Resources._2;
            }
            if (count == 3)
            {
                this.BackgroundImage = Properties.Resources._3;
            }
            if (count == 4)
            {
                this.BackgroundImage = Properties.Resources._4;
            }
            if (count == 5)
            {
                this.BackgroundImage = Properties.Resources._5;
            }
            if (count == 6)
            {
                this.BackgroundImage = Properties.Resources._6;
            }
            if (count == 7)
            {
                this.BackgroundImage = Properties.Resources._7;
            }
            if (count == 8)
            {
                this.BackgroundImage = Properties.Resources._8;
            }
            if (count == 9)
            {
                this.BackgroundImage = Properties.Resources._9;
            }
            if (count == 10)
            {
                this.BackgroundImage = Properties.Resources._10;
            }
            if (count == 11)
            {
                this.BackgroundImage = Properties.Resources._11;
            }
            if (count == 12)
            {
                this.BackgroundImage = Properties.Resources._12;
            }
            if (count == 13)
            {
                this.BackgroundImage = Properties.Resources._13;
            }
            if (count == 14)
            {
                this.BackgroundImage = Properties.Resources._14;
            }
            if (count == 15)
            {
                this.BackgroundImage = Properties.Resources._15;
            }
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            count++;
            if (count > 15)
            {
                count = 1;
            }
            if (count == 1)
            {
                this.BackgroundImage = Properties.Resources._1;
            }
            if (count == 2)
            {
                this.BackgroundImage = Properties.Resources._2;
            }
            if (count == 3)
            {
                this.BackgroundImage = Properties.Resources._3;
            }
            if (count == 4)
            {
                this.BackgroundImage = Properties.Resources._4;
            }
            if (count == 5)
            {
                this.BackgroundImage = Properties.Resources._5;
            }
            if (count == 6)
            {
                this.BackgroundImage = Properties.Resources._6;
            }
            if (count == 7)
            {
                this.BackgroundImage = Properties.Resources._7;
            }
            if (count == 8)
            {
                this.BackgroundImage = Properties.Resources._8;
            }
            if (count == 9)
            {
                this.BackgroundImage = Properties.Resources._9;
            }
            if (count == 10)
            {
                this.BackgroundImage = Properties.Resources._10;
            }
            if (count == 11)
            {
                this.BackgroundImage = Properties.Resources._11;
            }
            if (count == 12)
            {
                this.BackgroundImage = Properties.Resources._12;
            }
            if (count == 13)
            {
                this.BackgroundImage = Properties.Resources._13;
            }
            if (count == 14)
            {
                this.BackgroundImage = Properties.Resources._14;
            }
            if (count == 15)
            {
                this.BackgroundImage = Properties.Resources._15;
            }
        }
    }
}
