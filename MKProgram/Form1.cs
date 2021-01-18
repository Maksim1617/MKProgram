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
        static bool length63 = false;
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

                    //for (int c = 0; c < counter; c++)
                    //{
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
                    //}

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

                    //for (int c = 0; c < counter; c++)
                    //{
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
                    //}
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
            if (length63 == true)
            {
                int countBits = messageArray1.Count; // кількість біт в масиві
                int newBits = (int)Math.Ceiling(countBits / 51.0) * 12;
                int lastBits = countBits + newBits;
                if (newBits != 0) lastBits = lastBits + (51 - (countBits % 51));
                BitArray messageCoded = new BitArray(lastBits, false); // новий пустий масив біт

                for (int i = 0; i < countBits; i += 51)
                {
                    BitArray pol = new BitArray(51);

                    for (int j = 0; j < 51; j++)
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
                    int[] row = new int[51];
                    BitArray result = new BitArray(12); //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    BitArray result1 = new BitArray(12); //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    int counter = 0;
                    BitArray row0 = new BitArray(12);// { 0,1,1,1,0,1,0,0,1,0,0,0 };
                    row0[0] = false; row0[1] = true; row0[2] = true; row0[3] = true; row0[4] = false; row0[5] = true; row0[6] = false; row0[7] = false; row0[8] = true; row0[9] = false; row0[10] = false; row0[11] = false; // { 0,1,1,1,0,1,0,0,1,0,0,0 };

                    BitArray row1 = new BitArray(12); //{ 0,0,1,1,1,0,1,0,0,1,0,0 };
                    row1[0] = false; row1[1] = false; row1[2] = true; row1[3] = true; row1[4] = true; row1[5] = false; row1[6] = true; row1[7] = false; row1[8] = false; row1[9] = true; row1[10] = false; row1[11] = false; //{ 0,0,1,1,1,0,1,0,0,1,0,0 };

                    BitArray row2 = new BitArray(12); //{ 0,0,0,1,1,1,0,1,0,0,1,0 };
                    row2[0] = false; row2[1] = false; row2[2] = false; row2[3] = true; row2[4] = true; row2[5] = true; row2[6] = false; row2[7] = true; row2[8] = false; row2[9] = false; row2[10] = true; row2[11] = false; //{ 0,0,0,1,1,1,0,1,0,0,1,0 };

                    BitArray row3 = new BitArray(12); //{ 0,0,0,0,1,1,1,0,1,0,0,1 };
                    row3[0] = false; row3[1] = false; row3[2] = false; row3[3] = false; row3[4] = true; row3[5] = true; row3[6] = true; row3[7] = false; row3[8] = true; row3[9] = false; row3[10] = false; row3[11] = true; //{ 0,0,0,0,1,1,1,0,1,0,0,1 };

                    BitArray row4 = new BitArray(12); //{ 0,1,1,0,0,0,1,1,1,1,0,1 };
                    row4[0] = false; row4[1] = true; row4[2] = true; row4[3] = false; row4[4] = false; row4[5] = false; row4[6] = true; row4[7] = true; row4[8] = true; row4[9] = true; row4[10] = false; row4[11] = true; //{ 0,1,1,0,0,0,1,1,1,1,0,1 };

                    BitArray row5 = new BitArray(12); //{ 1,1,0,1,0,1,0,1,0,1,1,1 };
                    row5[0] = true; row5[1] = true; row5[2] = false; row5[3] = true; row5[4] = false; row5[5] = true; row5[6] = false; row5[7] = true; row5[8] = false; row5[9] = true; row5[10] = true; row5[11] = true; //{ 1,1,0,1,0,1,0,1,0,1,1,1 };

                    BitArray row6 = new BitArray(12); //{ 1,0,0,0,1,1,1,0,0,0,1,0 };
                    row6[0] = true; row6[1] = false; row6[2] = false; row6[3] = false; row6[4] = true; row6[5] = true; row6[6] = true; row6[7] = false; row6[8] = false; row6[9] = false; row6[10] = true; row6[11] = false; //{ 1,0,0,0,1,1,1,0,0,0,1,0 };*

                    BitArray row7 = new BitArray(12);// { 0,1,0,0,0,1,1,1,0,0,0,1 };
                    row7[0] = false; row7[1] = true; row7[2] = false; row7[3] = false; row7[4] = false; row7[5] = true; row7[6] = true; row7[7] = true; row7[8] = false; row7[9] = false; row7[10] = false; row7[11] = true; //{ 0,1,0,0,0,1,1,1,0,0,0,1 };

                    BitArray row8 = new BitArray(12); //{ 1,1,0,0,0,1,1,1,0,0,0,1 };
                    row8[0] = true; row8[1] = true; row8[2] = false; row8[3] = false; row8[4] = false; row8[5] = true; row8[6] = true; row8[7] = true; row8[8] = false; row8[9] = false; row8[10] = false; row8[11] = true; //{ 1,1,0,0,0,1,1,1,0,0,0,1 };

                    BitArray row9 = new BitArray(12); //{ 1,0,0,0,0,1,1,1,0,0,0,1 };
                    row9[0] = true; row9[1] = false; row9[2] = false; row9[3] = false; row9[4] = false; row9[5] = true; row9[6] = true; row9[7] = true; row9[8] = false; row9[9] = false; row9[10] = false; row9[11] = true; //{ 1,0,0,0,0,1,1,1,0,0,0,1 };

                    BitArray row10 = new BitArray(12); //{ 1,0,1,0,0,1,1,1,0,0,0,1 };
                    row10[0] = true; row10[1] = false; row10[2] = true; row10[3] = false; row10[4] = false; row10[5] = true; row10[6] = true; row10[7] = true; row10[8] = false; row10[9] = false; row10[10] = false; row10[11] = true; //{ 1,0,1,0,0,1,1,1,0,0,0,1 };

                    BitArray row11 = new BitArray(12); //{ 1,0,1,1,0,1,1,1,0,0,0,1 };
                    row11[0] = true; row11[1] = false; row11[2] = true; row11[3] = true; row11[4] = false; row11[5] = true; row11[6] = true; row11[7] = true; row11[8] = false; row11[9] = false; row11[10] = false; row11[11] = true; //{ 1,0,1,1,0,1,1,1,0,0,0,1 };

                    BitArray row12 = new BitArray(12); //{ 1,0,1,1,1,1,1,1,0,0,0,1 };
                    row12[0] = true; row12[1] = false; row12[2] = true; row12[3] = true; row12[4] = true; row12[5] = true; row12[6] = true; row12[7] = true; row12[8] = false; row12[9] = false; row12[10] = false; row12[11] = true; //{ 1,0,1,1,1,1,1,1,0,0,0,1 };

                    BitArray row13 = new BitArray(12); //{ 1,0,1,1,1,0,1,1,0,0,0,1 };
                    row13[0] = true; row13[1] = false; row13[2] = true; row13[3] = true; row13[4] = true; row13[5] = false; row13[6] = true; row13[7] = true; row13[8] = false; row13[9] = false; row13[10] = false; row13[11] = true; //{ 1,0,1,1,1,0,1,1,0,0,0,1 };

                    BitArray row14 = new BitArray(12);// { 1,0,1,1,1,0,0,1,0,0,0,1 };
                    row14[0] = true; row14[1] = false; row14[2] = true; row14[3] = true; row14[4] = true; row14[5] = false; row14[6] = false; row14[7] = true; row14[8] = false; row14[9] = false; row14[10] = false; row14[11] = true; // { 1,0,1,1,1,0,0,1,0,0,0,1 };

                    BitArray row15 = new BitArray(12); //{ 1,0,1,1,1,0,0,0,0,0,0,1 };
                    row15[0] = true; row15[1] = false; row15[2] = true; row15[3] = true; row15[4] = true; row15[5] = false; row15[6] = false; row15[7] = false; row15[8] = false; row15[9] = false; row15[10] = false; row15[11] = true; //{ 1,0,1,1,1,0,0,0,0,0,0,1 };

                    BitArray row16 = new BitArray(12); //{ 1,0,1,1,1,0,0,0,1,0,0,1 };
                    row16[0] = true; row16[1] = false; row16[2] = true; row16[3] = true; row16[4] = true; row16[5] = false; row16[6] = false; row16[7] = false; row16[8] = true; row16[9] = false; row16[10] = false; row16[11] = true; //{ 1,0,1,1,1,0,0,0,1,0,0,1 };

                    BitArray row17 = new BitArray(12); //{ 1,0,1,1,1,0,0,0,1,1,0,1 };
                    row17[0] = true; row17[1] = false; row17[2] = true; row17[3] = true; row17[4] = true; row17[5] = false; row17[6] = false; row17[7] = false; row17[8] = true; row17[9] = true; row17[10] = false; row17[11] = true; //{ 1,0,1,1,1,0,0,0,1,1,0,1 };

                    BitArray row18 = new BitArray(12); //{ 1,0,1,1,1,0,0,0,1,1,1,1 };
                    row18[0] = true; row18[1] = false; row18[2] = true; row18[3] = true; row18[4] = true; row18[5] = false; row18[6] = false; row18[7] = false; row18[8] = true; row18[9] = true; row18[10] = true; row18[11] = true; //{ 1,0,1,1,1,0,0,0,1,1,1,1 };

                    BitArray row19 = new BitArray(12); //{ 1,0,1,1,1,0,0,0,1,1,1,0 };
                    row19[0] = true; row19[1] = false; row19[2] = true; row19[3] = true; row19[4] = true; row19[5] = false; row19[6] = false; row19[7] = false; row19[8] = true; row19[9] = true; row19[10] = true; row19[11] = false; //{ 1,0,1,1,1,0,0,0,1,1,1,0 };

                    BitArray row20 = new BitArray(12); //{ 0,1,0,1,1,1,0,0,0,1,1,1 };
                    row20[0] = false; row20[1] = true; row20[2] = false; row20[3] = true; row20[4] = true; row20[5] = true; row20[6] = false; row20[7] = false; row20[8] = false; row20[9] = true; row20[10] = true; row20[11] = true; //{ 0,1,0,1,1,1,0,0,0,1,1,1 };

                    BitArray row21 = new BitArray(12);// { 1,1,0,0,1,0,1,0,1,0,1,0 };
                    row21[0] = true; row21[1] = true; row21[2] = false; row21[3] = false; row21[4] = true; row21[5] = false; row21[6] = true; row21[7] = false; row21[8] = true; row21[9] = false; row21[10] = true; row21[11] = false; // { 1,1,0,0,1,0,1,0,1,0,1,0 };

                    BitArray row22 = new BitArray(12); //{ 0,1,1,0,0,1,0,1,0,1,0,1 };
                    row22[0] = false; row22[1] = true; row22[2] = true; row22[3] = false; row22[4] = false; row22[5] = true; row22[6] = false; row22[7] = true; row22[8] = false; row22[9] = true; row22[10] = false; row22[11] = true; //{ 0,1,1,0,0,1,0,1,0,1,0,1 };

                    BitArray row23 = new BitArray(12); //{ 1,1,0,1,0,1,1,0,0,0,1,1 };
                    row23[0] = true; row23[1] = true; row23[2] = false; row23[3] = true; row23[4] = false; row23[5] = true; row23[6] = true; row23[7] = false; row23[8] = false; row23[9] = false; row23[10] = true; row23[11] = true; //{ 1,1,0,1,0,1,1,0,0,0,1,1 };

                    BitArray row24 = new BitArray(12); //{ 1,0,0,0,1,1,1,1,1,0,0,0 };
                    row24[0] = true; row24[1] = false; row24[2] = false; row24[3] = false; row24[4] = true; row24[5] = true; row24[6] = true; row24[7] = true; row24[8] = true; row24[9] = false; row24[10] = false; row24[11] = false;  //{ 1,0,0,0,1,1,1,1,1,0,0,0 };

                    BitArray row25 = new BitArray(12); //{ 0,1,0,0,0,1,1,1,1,1,0,0 };
                    row25[0] = false; row25[1] = true; row25[2] = false; row25[3] = false; row25[4] = false; row25[5] = true; row25[6] = true; row25[7] = true; row25[8] = true; row25[9] = true; row25[10] = false; row25[11] = false;  //{ 0,1,0,0,0,1,1,1,1,1,0,0 };

                    BitArray row26 = new BitArray(12); //{ 0,0,1,0,0,0,1,1,1,1,1,0 };
                    row26[0] = false; row26[1] = false; row26[2] = true; row26[3] = false; row26[4] = false; row26[5] = false; row26[6] = true; row26[7] = true; row26[8] = true; row26[9] = true; row26[10] = true; row26[11] = false; //{ 0,0,1,0,0,0,1,1,1,1,1,0 };

                    BitArray row27 = new BitArray(12); //{ 0,0,0,1,0,0,0,1,1,1,1,1 };
                    row27[0] = false; row27[1] = false; row27[2] = false; row27[3] = true; row27[4] = false; row27[5] = false; row27[6] = false; row27[7] = true; row27[8] = true; row27[9] = true; row27[10] = true; row27[11] = true; //{ 0,0,0,1,0,0,0,1,1,1,1,1 };

                    BitArray row28 = new BitArray(12); // { 1,1,1,0,1,1,0,0,0,1,1,0 };
                    row28[0] = true; row28[1] = true; row28[2] = true; row28[3] = false; row28[4] = true; row28[5] = true; row28[6] = false; row28[7] = false; row28[8] = false; row28[9] = true; row28[10] = true; row28[11] = false; // { 1,1,1,0,1,1,0,0,0,1,1,0 };

                    BitArray row29 = new BitArray(12); //{ 0,1,1,1,0,1,1,0,0,0,1,1 };
                    row29[0] = false; row29[1] = true; row29[2] = true; row29[3] = true; row29[4] = false; row29[5] = true; row29[6] = true; row29[7] = false; row29[8] = false; row29[9] = false; row29[10] = true; row29[11] = true; //{ 0,1,1,1,0,1,1,0,0,0,1,1 };

                    BitArray row30 = new BitArray(12); //{ 1,1,0,1,1,1,1,1,1,0,0,0 };
                    row30[0] = true; row30[1] = true; row30[2] = false; row30[3] = true; row30[4] = true; row30[5] = true; row30[6] = true; row30[7] = true; row30[8] = true; row30[9] = false; row30[10] = false; row30[11] = false; //{ 1,1,0,1,1,1,1,1,1,0,0,0 };

                    BitArray row31 = new BitArray(12); //{ 0,1,1,0,1,1,1,1,1,1,0,0 }; 
                    row31[0] = false; row31[1] = true; row31[2] = true; row31[3] = false; row31[4] = true; row31[5] = true; row31[6] = true; row31[7] = true; row31[8] = true; row31[9] = true; row31[10] = false; row31[11] = false;  //{ 0,1,1,0,1,1,1,1,1,1,0,0 };

                    BitArray row32 = new BitArray(12); //{ 0,0,1,1,0,1,1,1,1,1,1,0 };
                    row32[0] = false; row32[1] = false; row32[2] = true; row32[3] = true; row32[4] = false; row32[5] = true; row32[6] = true; row32[7] = true; row32[8] = true; row32[9] = true; row32[10] = true; row32[11] = false; //{ 0,0,1,1,0,1,1,1,1,1,1,0 };

                    BitArray row33 = new BitArray(12); //{ 0,0,0,1,1,0,1,1,1,1,1,1 };
                    row33[0] = false; row33[1] = false; row33[2] = false; row33[3] = true; row33[4] = true; row33[5] = false; row33[6] = true; row33[7] = true; row33[8] = true; row33[9] = true; row33[10] = true; row33[11] = true; //{ 0,0,0,1,1,0,1,1,1,1,1,1 };

                    BitArray row34 = new BitArray(12); //{ 1,1,1,0,1,0,0,1,0,1,1,0 };
                    row34[0] = true; row34[1] = true; row34[2] = true; row34[3] = false; row34[4] = true; row34[5] = false; row34[6] = false; row34[7] = true; row34[8] = false; row34[9] = true; row34[10] = true; row34[11] = false; //{ 1,1,1,0,1,0,0,1,0,1,1,0 };

                    BitArray row35 = new BitArray(12);// { 0,1,1,1,0,1,0,0,1,0,1,1 };
                    row35[0] = false; row35[1] = true; row35[2] = true; row35[3] = true; row35[4] = false; row35[5] = true; row35[6] = false; row35[7] = false; row35[8] = true; row35[9] = false; row35[10] = true; row35[11] = true; // { 0,1,1,1,0,1,0,0,1,0,1,1 };

                    BitArray row36 = new BitArray(12); //{ 1,1,0,1,1,1,1,0,1,1,0,0 };
                    row36[0] = true; row36[1] = true; row36[2] = false; row36[3] = true; row36[4] = true; row36[5] = true; row36[6] = true; row36[7] = false; row36[8] = true; row36[9] = true; row36[10] = false; row36[11] = false; //{ 1,1,0,1,1,1,1,0,1,1,0,0 };

                    BitArray row37 = new BitArray(12); //{ 0,1,1,0,1,1,1,1,0,1,1,0 };
                    row37[0] = false; row37[1] = true; row37[2] = true; row37[3] = false; row37[4] = true; row37[5] = true; row37[6] = true; row37[7] = true; row37[8] = false; row37[9] = true; row37[10] = true; row37[11] = false; //{ 0,1,1,0,1,1,1,1,0,1,1,0 };

                    BitArray row38 = new BitArray(12); //{ 0,0,1,1,0,1,1,1,1,0,1,1 };
                    row38[0] = false; row38[1] = false; row38[2] = true; row38[3] = true; row38[4] = false; row38[5] = true; row38[6] = true; row38[7] = true; row38[8] = true; row38[9] = false; row38[10] = true; row38[11] = true; //{ 0,0,1,1,0,1,1,1,1,0,1,1 };

                    BitArray row39 = new BitArray(12); //{ 1,1,1,1,1,1,1,1,0,1,0,0 };
                    row39[0] = true; row39[1] = true; row39[2] = true; row39[3] = true; row39[4] = true; row39[5] = true; row39[6] = true; row39[7] = true; row39[8] = false; row39[9] = true; row39[10] = false; row39[11] = false; //{ 1,1,1,1,1,1,1,1,0,1,0,0 };

                    BitArray row40 = new BitArray(12); //{ 0,1,1,1,1,1,1,1,1,0,1,0 };
                    row40[0] = false; row40[1] = true; row40[2] = true; row40[3] = true; row40[4] = true; row40[5] = true; row40[6] = true; row40[7] = true; row40[8] = true; row40[9] = false; row40[10] = true; row40[11] = false; //{ 0,1,1,1,1,1,1,1,1,0,1,0 };

                    BitArray row41 = new BitArray(12); //{ 0,0,1,1,1,1,1,1,1,1,0,1 };
                    row41[0] = false; row41[1] = false; row41[2] = true; row41[3] = true; row41[4] = true; row41[5] = true; row41[6] = true; row41[7] = true; row41[8] = true; row41[9] = true; row41[10] = false; row41[11] = true; //{ 0,0,1,1,1,1,1,1,1,1,0,1 };

                    BitArray row42 = new BitArray(12); //{ 1,1,1,1,1,0,1,1,0,1,1,1 };
                    row42[0] = true; row42[1] = true; row42[2] = true; row42[3] = true; row42[4] = true; row42[5] = false; row42[6] = true; row42[7] = true; row42[8] = false; row42[9] = true; row42[10] = true; row42[11] = true; //{ 1,1,1,1,1,0,1,1,0,1,1,1 };

                    BitArray row43 = new BitArray(12); //{ 1,0,0,1,1,0,0,1,0,0,1,0 };
                    row43[0] = true; row43[1] = false; row43[2] = false; row43[3] = true; row43[4] = true; row43[5] = false; row43[6] = false; row43[7] = true; row43[8] = false; row43[9] = false; row43[10] = true; row43[11] = false; //{ 1,0,0,1,1,0,0,1,0,0,1,0 };

                    BitArray row44 = new BitArray(12);// { 0,1,0,0,1,1,0,0,0,0,0,1 };
                    row44[0] = false; row44[1] = true; row44[2] = false; row44[3] = false; row44[4] = true; row44[5] = true; row44[6] = false; row44[7] = false; row44[8] = false; row44[9] = false; row44[10] = false; row44[11] = true; // { 0,1,0,0,1,1,0,0,0,0,0,1 };

                    BitArray row45 = new BitArray(12); //{ 1,1,0,0,1,0,1,0,1,0,0,1 };
                    row45[0] = true; row45[1] = true; row45[2] = false; row45[3] = false; row45[4] = true; row45[5] = false; row45[6] = true; row45[7] = false; row45[8] = true; row45[9] = false; row45[10] = false; row45[11] = true; //{ 1,1,0,0,1,0,1,0,1,0,0,1 };

                    BitArray row46 = new BitArray(12); //{ 1,0,0,0,0,0,0,1,1,1,0,1 };
                    row46[0] = true; row46[1] = false; row46[2] = false; row46[3] = false; row46[4] = false; row46[5] = false; row46[6] = false; row46[7] = true; row46[8] = true; row46[9] = true; row46[10] = false; row46[11] = true;  //{ 1,0,0,0,0,0,0,1,1,1,0,1 };

                    BitArray row47 = new BitArray(12); //{ 1,0,1,0,0,1,0,0,0,1,1,1 };
                    row47[0] = true; row47[1] = false; row47[2] = true; row47[3] = false; row47[4] = false; row47[5] = true; row47[6] = false; row47[7] = false; row47[8] = false; row47[9] = true; row47[10] = true; row47[11] = true; //{ 1,0,1,0,0,1,0,0,0,1,1,1 };

                    BitArray row48 = new BitArray(12); //{ 1,0,1,1,0,1,1,0,1,0,1,0 };
                    row48[0] = true; row48[1] = false; row48[2] = true; row48[3] = true; row48[4] = false; row48[5] = true; row48[6] = true; row48[7] = false; row48[8] = true; row48[9] = false; row48[10] = true; row48[11] = false; //{ 1,0,1,1,0,1,1,0,1,0,1,0 };

                    BitArray row49 = new BitArray(12); //{ 0,1,0,1,1,0,1,1,0,1,0,1 };
                    row49[0] = false; row49[1] = true; row49[2] = false; row49[3] = true; row49[4] = true; row49[5] = false; row49[6] = true; row49[7] = true; row49[8] = false; row49[9] = true; row49[10] = false; row49[11] = true; //{ 0,1,0,1,1,0,1,1,0,1,0,1 };

                    BitArray row50 = new BitArray(12); //{ 1,1,0,0,1,0,0,1,0,0,1,1 };
                    row50[0] = true; row50[1] = true; row50[2] = false; row50[3] = false; row50[4] = true; row50[5] = false; row50[6] = false; row50[7] = true; row50[8] = false; row50[9] = false; row50[10] = true; row50[11] = true; //{ 1,1,0,0,1,0,0,1,0,0,1,1 }; 

                    BitArray res = new BitArray(63);
                    for (int n = 0; n < 51; n++)
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

                    //for (int c = 0; c < counter-1; c++)
                    //{
                    if (row[0] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row0[l];
                        }
                    }
                    if (row[1] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row1[l];
                        }
                    }
                    if (row[2] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row2[l];
                        }
                    }
                    if (row[3] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row3[l];
                        }
                    }
                    if (row[4] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row4[l];
                        }
                    }
                    if (row[5] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row5[l];
                        }
                    }
                    if (row[6] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row6[l];
                        }
                    }
                    if (row[7] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row7[l];
                        }
                    }
                    if (row[8] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row8[l];
                        }
                    }
                    if (row[9] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row9[l];
                        }
                    }
                    if (row[10] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row10[l];
                        }
                    }
                    if (row[11] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row11[l];
                        }
                    }
                    if (row[12] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row12[l];
                        }
                    }
                    if (row[13] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row13[l];
                        }
                    }
                    if (row[14] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row14[l];
                        }
                    }
                    if (row[15] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row15[l];
                        }
                    }
                    if (row[16] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row16[l];
                        }
                    }
                    if (row[17] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row17[l];
                        }
                    }
                    if (row[18] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row18[l];
                        }
                    }
                    if (row[19] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row19[l];
                        }
                    }
                    if (row[20] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row20[l];
                        }
                    }
                    if (row[21] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row21[l];
                        }
                    }
                    if (row[22] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row22[l];
                        }
                    }
                    if (row[23] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row23[l];
                        }
                    }
                    if (row[24] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row24[l];
                        }
                    }
                    if (row[25] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row25[l];
                        }
                    }
                    if (row[26] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row26[l];
                        }
                    }
                    if (row[27] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row27[l];
                        }
                    }
                    if (row[28] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row28[l];
                        }
                    }
                    if (row[29] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row29[l];
                        }
                    }
                    if (row[30] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row30[l];
                        }
                    }
                    if (row[31] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row31[l];
                        }
                    }
                    if (row[32] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row32[l];
                        }
                    }
                    if (row[33] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row33[l];
                        }
                    }
                    if (row[34] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row34[l];
                        }
                    }
                    if (row[35] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row35[l];
                        }
                    }
                    if (row[36] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row36[l];
                        }
                    }
                    if (row[37] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row37[l];
                        }
                    }
                    if (row[38] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row38[l];
                        }
                    }
                    if (row[39] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row39[l];
                        }
                    }
                    if (row[40] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row40[l];
                        }
                    }
                    if (row[41] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row41[l];
                        }
                    }
                    if (row[42] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row42[l];
                        }
                    }
                    if (row[43] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row43[l];
                        }
                    }
                    if (row[44] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row44[l];
                        }
                    }
                    if (row[45] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row45[l];
                        }
                    }
                    if (row[46] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row46[l];
                        }
                    }
                    if (row[47] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row47[l];
                        }
                    }
                    if (row[48] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row48[l];
                        }
                    }
                    if (row[49] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row49[l];
                        }
                    }
                    if (row[50] == 1)
                    {
                        for (int l = 0; l < 12; l++)
                        {
                            result[l] ^= row50[l];
                        }
                    }
                    //}
                    for (int l = 0; l <= 50; l++)
                    {
                        res[l] = pol[l];
                    }
                    for (int l = 0; l < 12; l++)
                    {
                        res[l + 51] = result[l];
                    }
                    for (int k = 0; k < 63; k++)
                    {
                        messageCoded[k + (63 * (i / 51))] = res[k];
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
            if (length63 == true)
            {
                int countBits = messageArray2.Count; // кількість біт в масиві
                BitArray messageCodedd = new BitArray(counterBits, false);
                int count = 0;
                for (int i = 0; i < countBits; i += 63)
                {
                    if (count == 1)
                    {
                        break;
                    }
                    for (int j = 0; j < 51; j++)
                    {
                        if (j + (51 * (i / 63)) == counterBits)
                        {
                            count = 1;
                            break;
                        }
                        messageCodedd[j + (51 * (i / 63))] = messageArray2[j + (63 * (i / 63))];
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
            length63 = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            length15 = true;
            length31 = false;
            length63 = false;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            length63 = true;
            length15 = false;
            length31 = false;
        }
    }
}
