using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MKProgram
{
    public partial class Form1 : Form
    {
        BitArray messageArray1;
        BitArray messageCoded1;
        string pathout = "";
        bool Coding = false;
        bool Decoding = false;
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

        static BitArray MyCoding1(BitArray messageArray1)
        {
            int countBits = messageArray1.Count; // кількість біт в масиві
            int newBits = (int) Math.Ceiling(countBits / 7.0) * 8;
            int lastBits = countBits + newBits;
            if (newBits != 0) lastBits = lastBits + (7 - (countBits % 7));
            BitArray messageCoded = new BitArray(lastBits, false); // новий пустий масив біт

            for (int i = 0; i < countBits; i += 7)
            {
                BitArray pol = new BitArray(7);

                for (int j = 0; j < 7; j++)
                {
                    if (j + i >= countBits)
                    {
                        pol[j] = false;
                    }
                    else
                    {
                        pol[j] = messageArray1[j + i]; 
                    }
                }
                    int[] row = new int[7];
                    BitArray result = new BitArray(8); //{ 0, 0, 0, 0, 0, 0, 0, 0 };
                    BitArray result1 = new BitArray(8); //{ 0, 0, 0, 0, 0, 0, 0, 0 };
                    int counter = 0;
                    BitArray row0 = new BitArray(8);// { 1, 1, 1, 0, 1, 0, 0, 0 };
                    row0[0] = true; row0[1] = true; row0[2] = true; row0[3] = false; row0[4] = true; row0[5] = false; row0[6] = false; row0[7] = false; // { 1, 1, 1, 0, 1, 0, 0, 0 };
                    BitArray row1 = new BitArray(8); //{ 0, 1, 1, 1, 0, 1, 0, 0 };
                    row1[0] = false; row1[1] = true; row1[2] = true; row1[3] = true; row1[4] = false; row1[5] = true; row1[6] = false; row1[7] = false; //{ 0, 1, 1, 1, 0, 1, 0, 0 };
                    BitArray row2 = new BitArray(8); //{ 0, 0, 1, 1, 1, 0, 1, 0 };
                    row2[0] = false; row2[1] = false; row2[2] = true; row2[3] = true; row2[4] = true; row2[5] = false; row2[6] = true; row2[7] = false; //{ 0, 0, 1, 1, 1, 0, 1, 0 };
                    BitArray row3 = new BitArray(8); //{ 0, 0, 0, 1, 1, 1, 0, 1 };
                    row3[0] = false; row3[1] = false; row3[2] = false; row3[3] = true; row3[4] = true; row3[5] = true; row3[6] = false; row3[7] = true; //{ 0, 0, 0, 1, 1, 1, 0, 1 };
                    BitArray row4 = new BitArray(8); //{ 1, 1, 1, 0, 0, 1, 1, 0 };
                    row4[0] = true; row4[1] = true; row4[2] = true; row4[3] = false; row4[4] = false; row4[5] = true; row4[6] = true; row4[7] = false; //{ 1, 1, 1, 0, 0, 1, 1, 0 };
                    BitArray row5 = new BitArray(8); //{ 0, 1, 1, 1, 0, 0, 1, 1 };
                    row5[0] = false; row5[1] = true; row5[2] = true; row5[3] = true; row5[4] = false; row5[5] = false; row5[6] = true; row5[7] = true; //{ 0, 1, 1, 1, 0, 0, 1, 1 };
                    BitArray row6 = new BitArray(8); //{ 1, 1, 0, 1, 0, 0, 0, 1 };
                    row6[0] = true; row6[1] = true; row6[2] = false; row6[3] = true; row6[4] = false; row6[5] = false; row6[6] = false; row6[7] = true;
                    BitArray res = new BitArray(15);

                    for (int n = 0; n < 7; n++)
                    {
                        if (pol[n] == true)
                        {
                            row[n] = 1;
                            counter++;
                        }
                        if (pol[n] == false)
                        {
                            row[n] = 0;
                        }
                    }

                    for (int c = 0; c < counter; c++)
                    {
                        if (row[0] == 1)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                result[l] ^= row0[l];
                            }
                        }
                        if (row[1] == 1)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                result[l] ^= row1[l];
                            }
                        }
                        if (row[2] == 1)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                result[l] ^= row2[l];
                            }
                        }
                        if (row[3] == 1)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                result[l] ^= row3[l];
                            }
                        }
                        if (row[4] == 1)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                result[l] ^= row4[l];
                            }
                        }
                        if (row[5] == 1)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                result[l] ^= row5[l];
                            }
                        }
                        if (row[6] == 1)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                result[l] ^= row6[l];
                            }
                        }
                    }

                    for (int l = 0; l <= 6; l++) 
                    {
                        res[l] = pol[l];
                    }
                    for (int l = 0; l < 8; l++)
                    {
                        res[l + 7] = result[l];
                    }

                //for(int k = 0; k < 15; k++)
                //{
                //    messageCoded[k] = res[k];
                //}

                for (int k = 0; k < 15; k++)
                {
                    messageCoded[k + (15*(i/7))] = res[k];
                }
            }
            return messageCoded;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyCoding.f();
            MyCoding.text1 = textBox1.Text;

            radioButton1.Checked = true;
            radioButton6.Checked = true;
            radioButton3.Checked = true;
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
                // получаем выбранный файл
                string filename = openFileDialog1.FileName;
                // читаем файл в строку
                messageArray1 = ConvertFileToBitArray(filename);
                textBox1.Text = filename;   
            }
            if(Decoding == true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = openFileDialog1.FileName;
                textBox1.Text = filename;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Coding == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;
                pathout = filename;
                textBox2.Text = filename;
            }
            if (Decoding == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
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
            if (Coding == true)
            {
                messageCoded1 = MyCoding1(messageArray1);
                System.IO.File.WriteAllBytes(pathout, BitArrayToBytes(messageCoded1));
                MessageBox.Show("Файл сохранен");
            }
        }
    }
}
