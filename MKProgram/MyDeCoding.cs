using System;
using System.Collections;
using System.Windows.Forms;

namespace MKProgram
{
    class MyDeCoding
    {
        public BitArray DeCoding(BitArray messageArrayInFile, string length, ProgressBar progressBar1)
        {
            int countBits = messageArrayInFile.Count; // кількість біт в масиві
            int count = 0;
            progressBar1.Value = 0;

            if (length == "length15")
            {
                progressBar1.Maximum = countBits;

                // розрахунок змінної counterBits для випадку length15 == true
                /////////////////////////////////////////////
                int a = (int)(countBits / 15.0) * 8;
                int b = countBits - (countBits % 15);
                int lastBits = b - a;
                lastBits = lastBits - (lastBits % 8);
                int counterBits = lastBits; /// <- !!!!!!!!
                BitArray messageCodedd = new BitArray(counterBits, false); // тут використовується НЕ static int counterBits;
                /////////////////////////////////////////////

                for (int i = 0; i < countBits; i += 15) //Виписуємо 7 інформаційних біт з 15 закодованих
                {
                    progressBar1.Value = i;
                    Application.DoEvents();

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
                        messageCodedd[j + (7 * (i / 15))] = messageArrayInFile[j + (15 * (i / 15))];
                    }
                }
                return messageCodedd;
            }
            if (length == "length31")
            {
                progressBar1.Maximum = countBits;

                // розрахунок змінної counterBits для випадку length31 == true
                /////////////////////////////////////////////
                int a = (int)(countBits / 31.0) * 10;
                int b = countBits - (countBits % 31);
                int lastBits = b - a;
                lastBits = lastBits - (lastBits % 8);
                int counterBits = lastBits; /// <- !!!!!!!!
                BitArray messageCodedd = new BitArray(counterBits, false); // тут використовується НЕ static int counterBits;
                /////////////////////////////////////////////

                for (int i = 0; i < countBits; i += 31) //Виписуємо 21 інформаційний біт з 31 закодованих
                {
                    progressBar1.Value = i;
                    Application.DoEvents();

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
                        messageCodedd[j + (21 * (i / 31))] = messageArrayInFile[j + (31 * (i / 31))];
                    }
                }
                return messageCodedd;
            }
            if (length == "length63")
            {
                progressBar1.Maximum = countBits;

                // розрахунок змінної counterBits для випадку length63 == true
                /////////////////////////////////////////////
                int a = (int)(countBits / 63.0) * 12;
                int b = countBits - (countBits % 63);
                int lastBits = b - a;
                lastBits = lastBits - (lastBits % 8);
                int counterBits = lastBits; /// <- !!!!!!!!
                BitArray messageCodedd = new BitArray(counterBits, false); // тут використовується НЕ static int counterBits;
                /////////////////////////////////////////////

                for (int i = 0; i < countBits; i += 63) //Виписуємо 51 інформаційний біт з 63 закодованих
                {
                    progressBar1.Value = i;
                    Application.DoEvents();

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
                        messageCodedd[j + (51 * (i / 63))] = messageArrayInFile[j + (63 * (i / 63))];
                    }
                }
                return messageCodedd;
            }
            else
                return messageArrayInFile;

        }

    }
}
