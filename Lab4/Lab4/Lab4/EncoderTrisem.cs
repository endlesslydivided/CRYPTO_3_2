using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class EncoderTrisem
    {
        public string alphabet;
        public char[,] tableAlphabet;
        public string keyWord;
        public int height;
        public int width;

        public EncoderTrisem(string alphabet, string keyWord)
        {
            this.alphabet = alphabet;
            this.tableAlphabet = makeTable(alphabet,keyWord);
            this.keyWord = keyWord;
        }

        public char[,] makeTable(string alphabet,string keyWord)
        {
            this.height = 5;
            this.width = 6;
            int k = 0;
            int heightA = 0,widthA = 0;
            char[,] finalTable = new char[height, width];
            for(int iterY = 0; iterY < height;iterY++)
            {
                if (k >= keyWord.Length)
                {
                    heightA = iterY;
                    break;
                }
                for (int iterX = 0; iterX < width;iterX++)
                {
                    if(k < keyWord.Length)
                    {
                        if(!containSymbol(finalTable, keyWord[k],height,width))
                        {
                            finalTable[iterY, iterX] = keyWord[k++];
                        }
                    }
                    else
                    {
                        widthA = iterX;
                        break;
                    }
                }
               
            }
            for(int iter = 0;iter < alphabet.Length; iter++)
            {
                if(widthA  >= width)
                {
                    heightA++;
                    widthA = 0;
                }
                if(heightA < height)
                {
                    if (!containSymbol(finalTable, alphabet[iter], height, width))
                    {
                        finalTable[heightA, widthA++] = alphabet[iter];
                    }
                }
                else
                {
                    break;
                }
            }
            return finalTable;
        }
        
        private bool containSymbol(char[,] table, char symbol,int height, int width)
        {
            for (int iterY = 0; iterY < height; iterY++)
            {
                for (int iterX = 0; iterX < width; iterX++)
                {
                    if (table[iterY, iterX] == symbol)
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return false;
        }

        public string encode(string text)
        {
            StringBuilder stringFinal = new StringBuilder();

            foreach(char x in text)
            {
                for(int iterY = 0; iterY < height;iterY++)
                {
                    for (int iterX = 0; iterX < width; iterX++)
                    {
                        if(x == tableAlphabet[iterY, iterX])
                        {
                            if(iterY + 1 < height)
                            {
                                if(tableAlphabet[iterY + 1, iterX] != '\0')
                                    stringFinal.Append(tableAlphabet[iterY + 1, iterX]);
                                else
                                    stringFinal.Append(tableAlphabet[0, iterX]);
                            }
                            else
                            {
                                stringFinal.Append(tableAlphabet[0, iterX]);
                            }
                        }
                    }
                }
            }
            return stringFinal.ToString();
        }

        public string decode(string text)
        {
            StringBuilder stringFinal = new StringBuilder();

            foreach (char x in text)
            {
                for (int iterY = 0; iterY < height; iterY++)
                {
                    for (int iterX = 0; iterX < width; iterX++)
                    {
                        if (x == tableAlphabet[iterY, iterX])
                        {
                            if (iterY - 1 >= 0)
                            {
                                if (tableAlphabet[iterY - 1, iterX] != '\0')
                                    stringFinal.Append(tableAlphabet[iterY - 1, iterX]);
                                else
                                    stringFinal.Append(tableAlphabet[height - 1, iterX]);
                            }
                            else
                            {
                                stringFinal.Append(tableAlphabet[height - 1, iterX]);
                            }
                        }
                    }
                }
            }
            return stringFinal.ToString();
        }

        public void printMatrix(char[,] input)
        {
            for (int w = 0; w < height ; w++)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.Write($"{(input[w, i] == ' ' ? '*' : input[w, i])} ");
                }
                Console.Write($"\n");

            }
            Console.Write($"\n");
        }
    }
}
