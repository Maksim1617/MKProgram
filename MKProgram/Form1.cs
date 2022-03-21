using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CyclicCodes;

namespace MKProgram
{
    public partial class Form1 : Form
    {
        static int counterBits;
        BitArray messageArray1;
        BitArray messageArray2;
        string pathout = "";
        bool Coding = false;
        bool Decoding = false;
        static string leng;
        MyCoding myCoding = new MyCoding();
        MyDeCoding myDeCoding = new MyDeCoding();
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Aquamarine;

        }
        static BitArray ConvertFileToBitArray(string path) // переводимо текст з файла в масив бітів
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            BitArray messageArray = new BitArray(fileBytes);

            return messageArray;
        }
        public static byte[] BitArrayToBytes(System.Collections.BitArray messageCoded1) // переводимо масив бітів в масив байтів
        {
            if (messageCoded1.Length == 0)
            {
                throw new System.ArgumentException("must have at least length 1", "bitarray");
            }

            int num_bytes = messageCoded1.Length / 8;

            if (messageCoded1.Length % 8 != 0)
            {
                num_bytes += 1;
            }

            var bytes = new byte[num_bytes];
            messageCoded1.CopyTo(bytes, 0);
            return bytes;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton6.Checked = true;
            radioButton3.Checked = true;

            progressBar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Coding == true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // отримуємо вибраний файл
                string filename = openFileDialog1.FileName;
                // читаем файл в строку
                messageArray1 = ConvertFileToBitArray(filename);
                counterBits = messageArray1.Count;
                textBox1.Text = filename;
            }
            if (Decoding == true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // отримуємо вибраний файл
                string filename = openFileDialog1.FileName;
                messageArray2 = ConvertFileToBitArray(filename);
                textBox1.Text = filename;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Coding == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // отримуємо файл для збереження
                string filename = saveFileDialog1.FileName;
                pathout = filename;
                textBox2.Text = filename;
            }
            if (Decoding == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // отримуємо файл для збереження
                string filename = saveFileDialog1.FileName;
                pathout = filename;
                textBox2.Text = filename;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Coding = true;
            Decoding = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Decoding = true;
            Coding = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Необхідно вибрати вхідний файл!");
                return;
            }
            if (File.Exists(textBox1.Text) == false)
            {
                MessageBox.Show("Відсутній вхідний файл \n(вкажіть правильно шлях до вхідного файлу)");
                return;
            }
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Необхідно вибрати вихідний файл!");
                return;
            }

            string pathInFile = textBox1.Text;
            BitArray messageArrayInFile = ConvertFileToBitArray(pathInFile);

            string pathOutFile = textBox2.Text;

            progressBar1.Visible = true;
            if (Coding == true)
            {
                BitArray messageCoded = myCoding.Coding(messageArrayInFile, leng, progressBar1);

                System.IO.File.WriteAllBytes(pathOutFile, BitArrayToBytes(messageCoded));
                MessageBox.Show("Вихідний файл закодовано \nі збережено у вихідний файл");
            }
            if (Decoding == true)
            {
                BitArray messageDecoded = myDeCoding.DeCoding(messageArrayInFile, leng, progressBar1);

                System.IO.File.WriteAllBytes(pathOutFile, BitArrayToBytes(messageDecoded));
                MessageBox.Show("Вихідний файл декодовано \nі збережено у вихідний файл");
            }
            progressBar1.Visible = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            leng = "length31";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            leng = "length15";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            leng = "length63";
        }

        private void button_Info_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}