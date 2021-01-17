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
        static int counterBits;
        BitArray messageArray1;
        BitArray messageCoded1;
        BitArray messageArray2;
        BitArray messageDecoded1;
        string pathout = "";
        bool Coding = false;
        bool Decoding = false;
        static bool length15 = false;
        static bool length31 = false;
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
            if ( length15 == true)
            {
                int countBits = messageArray1.Count; // кількість біт в масиві
                int newBits = (int)Math.Ceiling(countBits / 7.0) * 8;
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

                    for (int k = 0; k < 15; k++)
                    {
                        messageCoded[k + (15 * (i / 7))] = res[k];
                    }
                }
                return messageCoded;
            }
            if (length31 == true)
            {
                int countBits = messageArray1.Count; // кількість біт в масиві
                int newBits = (int)Math.Ceiling(countBits / 21.0) * 10;
                int lastBits = countBits + newBits;
                if (newBits != 0) lastBits = lastBits + (21 - (countBits % 21));
                BitArray messageCoded = new BitArray(lastBits, false); // новий пустий масив біт

                for (int i = 0; i < countBits; i += 21)
                {
                    BitArray pol = new BitArray(21);

                    for (int j = 0; j < 21; j++)
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
                    int[] row = new int[21];
                    BitArray result = new BitArray(10); //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    BitArray result1 = new BitArray(10); //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    int counter = 0;
                    BitArray row0 = new BitArray(10);// { 0, 0, 0, 1, 0, 0, 0, 1, 1, 0 };
                    row0[0] = false; row0[1] = false; row0[2] = false; row0[3] = true; row0[4] = false; row0[5] = false; row0[6] = false; row0[7] = true; row0[8] = true; row0[9] = false; // { 0, 0, 0, 1, 0, 0, 0, 1, 1, 0 };

                    BitArray row1 = new BitArray(10); //{ 0,0,0,0,1,0,0,0,1,1 };
                    row1[0] = false; row1[1] = false; row1[2] = false; row1[3] = false; row1[4] = true; row1[5] = false; row1[6] = false; row1[7] = false; row1[8] = true; row1[9] = true; //{ 0,0,0,0,1,0,0,0,1,1 };

                    BitArray row2 = new BitArray(10); //{ 1,1,1,1,0,0,1,0,1,0 };
                    row2[0] = true; row2[1] = true; row2[2] = true; row2[3] = true; row2[4] = false; row2[5] = false; row2[6] = true; row2[7] = false; row2[8] = true; row2[9] = false; //{ 1,1,1,1,0,0,1,0,1,0 };

                    BitArray row3 = new BitArray(10); //{ 0,1,1,1,1,0,0,1,0,1 };
                    row3[0] = false; row3[1] = true; row3[2] = true; row3[3] = true; row3[4] = true; row3[5] = false; row3[6] = false; row3[7] = true; row3[8] = false; row3[9] = true; //{ 0,1,1,1,1,0,0,1,0,1 };

                    BitArray row4 = new BitArray(10); //{ 1,1,0,0,1,0,1,0,0,1 };
                    row4[0] = true; row4[1] = true; row4[2] = false; row4[3] = false; row4[4] = true; row4[5] = false; row4[6] = true; row4[7] = false; row4[8] = false; row4[9] = true;//{ 1,1,0,0,1,0,1,0,0,1 };

                    BitArray row5 = new BitArray(10); //{ 1,0,0,1,0,0,1,1,1,1 };
                    row5[0] = true; row5[1] = false; row5[2] = false; row5[3] = true; row5[4] = false; row5[5] = false; row5[6] = true; row5[7] = true; row5[8] = true; row5[9] = true; //{ 1,0,0,1,0,0,1,1,1,1 };

                    BitArray row6 = new BitArray(10); //{ 1,0,1,1,1,1,1,1,0,0 };
                    row6[0] = true; row6[1] = false; row6[2] = true; row6[3] = true; row6[4] = true; row6[5] = true; row6[6] = true; row6[7] = true; row6[8] = false; row6[9] = false; //{ 1,0,1,1,1,1,1,1,0,0 };

                    BitArray row7 = new BitArray(10);// { 0,1,0,1,1,1,1,1,1,0 };
                    row7[0] = false; row7[1] = true; row7[2] = false; row7[3] = true; row7[4] = true; row7[5] = true; row7[6] = true; row7[7] = true; row7[8] = true; row7[9] = false; // { 0,1,0,1,1,1,1,1,1,0 };

                    BitArray row8 = new BitArray(10); //{ 0,0,1,0,1,1,1,1,1,1 };
                    row8[0] = false; row8[1] = false; row8[2] = true; row8[3] = false; row8[4] = true; row8[5] = true; row8[6] = true; row8[7] = true; row8[8] = true; row8[9] = true; //{ 0,0,1,0,1,1,1,1,1,1 };

                    BitArray row9 = new BitArray(10); //{ 1,1,1,0,0,0,0,1,0,0 };
                    row9[0] = true; row9[1] = true; row9[2] = true; row9[3] = false; row9[4] = false; row9[5] = false; row9[6] = false; row9[7] = true; row9[8] = false; row9[9] = false;//{ 1,1,1,0,0,0,0,1,0,0 };

                    BitArray row10 = new BitArray(10); //{ 0,1,1,1,0,0,0,0,1,0 };
                    row10[0] = false; row10[1] = true; row10[2] = true; row10[3] = true; row10[4] = false; row10[5] = false; row10[6] = false; row10[7] = false; row10[8] = true; row10[9] = false; //{ 0,1,1,1,0,0,0,0,1,0 };

                    BitArray row11 = new BitArray(10); //{ 0,0,1,1,1,0,0,0,0,1 };
                    row11[0] = false; row11[1] = false; row11[2] = true; row11[3] = true; row11[4] = true; row11[5] = false; row11[6] = false; row11[7] = false; row11[8] = false; row11[9] = true; //{ 0,0,1,1,1,0,0,0,0,1 };

                    BitArray row12 = new BitArray(10); //{ 1,1,1,0,1,0,1,0,1,1 };
                    row12[0] = true; row12[1] = true; row12[2] = true; row12[3] = false; row12[4] = true; row12[5] = false; row12[6] = true; row12[7] = false; row12[8] = true; row12[9] = true; //{ 1,1,1,0,1,0,1,0,1,1 };

                    BitArray row13 = new BitArray(10); //{ 1,0,0,0,0,0,1,1,1,0 };
                    row13[0] = true; row13[1] = false; row13[2] = false; row13[3] = false; row13[4] = false; row13[5] = false; row13[6] = true; row13[7] = true; row13[8] = true; row13[9] = false; //{ 1,0,0,0,0,0,1,1,1,0 };

                    BitArray row14 = new BitArray(10);// { 0,1,0,0,0,0,0,1,1,1 };
                    row14[0] = false; row14[1] = true; row14[2] = false; row14[3] = false; row14[4] = false; row14[5] = false; row14[6] = false; row14[7] = true; row14[8] = true; row14[9] = true; // { 0,1,0,0,0,0,0,1,1,1 };

                    BitArray row15 = new BitArray(10); //{ 1,1,0,1,0,1,1,0,0,0 };
                    row15[0] = true; row15[1] = true; row15[2] = false; row15[3] = true; row15[4] = false; row15[5] = true; row15[6] = true; row15[7] = false; row15[8] = false; row15[9] = false; //{ 1,1,0,1,0,1,1,0,0,0 };

                    BitArray row16 = new BitArray(10); //{ 0,1,1,0,1,0,1,1,0,0 };
                    row16[0] = false; row16[1] = true; row16[2] = true; row16[3] = false; row16[4] = true; row16[5] = false; row16[6] = true; row16[7] = true; row16[8] = false; row16[9] = false; //{ 0,1,1,0,1,0,1,1,0,0 };

                    BitArray row17 = new BitArray(10); //{ 1,1,0,1,1,0,0,1,0,0 };
                    row17[0] = true; row17[1] = true; row17[2] = false; row17[3] = true; row17[4] = true; row17[5] = false; row17[6] = false; row17[7] = true; row17[8] = false; row17[9] = false; //{ 1,1,0,1,1,0,0,1,0,0 };

                    BitArray row18 = new BitArray(10); //{ 0,1,1,0,1,1,0,0,1,0 };
                    row18[0] = false; row18[1] = true; row18[2] = true; row18[3] = false; row18[4] = true; row18[5] = true; row18[6] = false; row18[7] = false; row18[8] = true; row18[9] = false; //{ 0,1,1,0,1,1,0,0,1,0 };

                    BitArray row19 = new BitArray(10); //{ 0,0,1,1,0,1,1,0,0,1 };
                    row19[0] = false; row19[1] = false; row19[2] = true; row19[3] = true; row19[4] = false; row19[5] = true; row19[6] = true; row19[7] = false; row19[8] = false; row19[9] = true; //{ 0,0,1,1,0,1,1,0,0,1 };

                    BitArray row20 = new BitArray(10); //{ 1,1,1,0,1,1,0,1,1,1 };
                    row20[0] = true; row20[1] = true; row20[2] = true; row20[3] = false; row20[4] = true; row20[5] = true; row20[6] = false; row20[7] = true; row20[8] = true; row20[9] = true; //{ 1,1,1,0,1,1,0,1,1,1 };

                    BitArray res = new BitArray(31);

                    for (int n = 0; n < 21; n++)
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
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row0[l];
                            }
                        }
                        if (row[1] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row1[l];
                            }
                        }
                        if (row[2] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row2[l];
                            }
                        }
                        if (row[3] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row3[l];
                            }
                        }
                        if (row[4] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row4[l];
                            }
                        }
                        if (row[5] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row5[l];
                            }
                        }
                        if (row[6] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row6[l];
                            }
                        }
                        if (row[7] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row7[l];
                            }
                        }
                        if (row[8] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row8[l];
                            }
                        }
                        if (row[9] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row9[l];
                            }
                        }
                        if (row[10] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row10[l];
                            }
                        }
                        if (row[11] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row11[l];
                            }
                        }
                        if (row[12] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row12[l];
                            }
                        }
                        if (row[13] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row13[l];
                            }
                        }
                        if (row[14] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row14[l];
                            }
                        }
                        if (row[15] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row15[l];
                            }
                        }
                        if (row[16] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row16[l];
                            }
                        }
                        if (row[17] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row17[l];
                            }
                        }
                        if (row[18] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row18[l];
                            }
                        }
                        if (row[19] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row19[l];
                            }
                        }
                        if (row[20] == 1)
                        {
                            for (int l = 0; l < 10; l++)
                            {
                                result[l] ^= row20[l];
                            }
                        }
                    }
                    for (int l = 0; l <= 20; l++)
                    {
                        res[l] = pol[l];
                    }
                    for (int l = 0; l < 10; l++)
                    {
                        res[l + 21] = result[l];
                    }

                    for (int k = 0; k < 31; k++)
                    {
                        messageCoded[k + (31 * (i / 21))] = res[k];
                    }
                }
                return messageCoded;
            }
            else
            {
                return messageArray1;
            }
        }

        static BitArray MyDeCoding(BitArray messageArray2)
        {
            if (length15 == true)
            {
                int countBits = messageArray2.Count; // кількість біт в масиві
                BitArray messageCodedd = new BitArray(counterBits, false);
                int count = 0;
                for (int i = 0; i < countBits; i += 15)
                {
                    if (count == 1)
                    {
                        break;
                    }
                    for (int j = 0; j < 7; j++)
                    {
                        if (j + (7 * (i / 15)) == counterBits)
                        {
                            count = 1;
                            break;
                        }
                        messageCodedd[j + (7 * (i / 15))] = messageArray2[j + (15 * (i / 15))];
                    }
                }
                return messageCodedd;
            }
            if (length31 == true)
            {
                int countBits = messageArray2.Count; // кількість біт в масиві
                BitArray messageCodedd = new BitArray(counterBits, false);
                int count = 0;
                for (int i = 0; i < countBits; i += 31)
                {
                    if (count == 1)
                    {
                        break;
                    }
                    for (int j = 0; j < 21; j++)
                    {
                        if (j + (21 * (i / 31)) == counterBits)
                        {
                            count = 1;
                            break;
                        }
                        messageCodedd[j + (21 * (i / 31))] = messageArray2[j + (31 * (i / 31))];
                    }
                }
                return messageCodedd;
            }
            else
            {
                return messageArray2;
            }
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
                counterBits = messageArray1.Count;
                textBox1.Text = filename;   
            }
            if(Decoding == true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
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
                //messageCoded1 = MyCoding2(messageArray1);
                messageCoded1 = MyCoding1(messageArray1);
                System.IO.File.WriteAllBytes(pathout, BitArrayToBytes(messageCoded1));
                MessageBox.Show("Файл сохранен");
            }
            if (Decoding == true)
            {
                //messageDecoded1 = MyDeCoding1(messageArray2);
                messageDecoded1 = MyDeCoding(messageArray2);
                System.IO.File.WriteAllBytes(pathout, BitArrayToBytes(messageDecoded1));
                MessageBox.Show("Файл сохранен");
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            length31 = true;
            length15 = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            length15 = true;
            length31 = false;
        }
    }
}
