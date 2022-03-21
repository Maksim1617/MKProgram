using System;
using System.Drawing;
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
            returnImage(count);
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            count++;
            if (count > 15)
            {
                count = 1;
            }
            returnImage(count);
        }

        private Image returnImage(int count)
        {
            return this.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("_" + count);
        }
    }
}