using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class SnakeEncrypter
    {
        private int tableHeight;
        private int tableWidth;
        private string text;


        public int TableHeight
        {
            get { return tableHeight; }
            set { tableHeight = value; }
        }

        public int TableWidth
        {
            get { return tableWidth; }
            set { tableWidth = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public SnakeEncrypter(int tableHeight, int tableWidth, string text)
        {
            TableHeight = tableHeight;
            TableWidth = tableWidth;
            Text = text;
        }

        public char[,] createMatrix(string input)
        {
            char[,] table = new char[tableWidth, tableHeight];
            int l = 0;
            for(int w = 0; w< tableWidth; w++)
            {
                for(int i = 0;i < tableHeight; i++)
                {
                    if (l == input.Length) break;
                    table[w, i] = input[l++];
                }
                if (l == input.Length) break;
            }
            return table;
        }

        public string createString(char[] input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int w = 0; w < input.Length; w++)
            {
                    stringBuilder.Append(input[w]);            
            }
            return stringBuilder.ToString();
        }

        public string createString(char[,] input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int w = 0; w < tableWidth; w++)
            {
                for(int l = 0; l < tableHeight ; l++)
                    stringBuilder.Append(input[w,l]);
            }
            return stringBuilder.ToString();
        }

        public void printMatrix(char[,] input)
        {
            for (int w = 0; w < tableWidth; w++)
            {
                for (int i = 0; i < tableHeight; i++)
                {
                    Console.Write($"{(input[w, i] == ' ' ? '*' : input[w, i])} ");
                }
                Console.Write($"\n");

            }
            Console.Write($"\n");
        }

        public string Encrypt()
        {
            char[,] table = createMatrix(text);
            char[] result = new char[text.Length];
            int x = 0, y = 0, l =0;

            while (true)
            {
                if (x < tableHeight)
                {
                    if (table[y, x] != '\0')
                        result[l++] = table[y, x++];
                    else
                    {
                        x++;
                    }
                }
                else
                {
                    x++;
                }

                while(x != 0)
                {
                     if (y < tableWidth && x < tableHeight)
                    {
                        if (table[y, x] != '\0')
                            result[l++] = table[y++, x--];
                        else
                        {
                            y++; x--;
                        }
                    }
                    else
                    {
                        y++; x--;
                    }
                }


                if(y < tableWidth)
                {
                    if (table[y, x] != '\0')
                        result[l++] = table[y++, x];
                    else
                    {
                        y++;
                    }
                }
                else
                {
                    y++;
                }

                while (y != 0)
                {
                    if (y < tableWidth && x < tableHeight)
                    {
                        if (table[y, x] != '\0')
                            result[l++] = table[y--, x++];
                        else
                        {
                            y--; x++;
                        }
                    }
                    else
                    {
                        y--; x++;
                    }
                }
                if (l == text.Length) break;
            }


            return createString(result);
        }


        public char[,] Decrypt(string input)
        {
            char[,] table = new char[tableWidth,tableHeight];
            int x = 0, y = 0, l = 0;
            int last = tableWidth * tableHeight - input.Length;
            int downY = (last % tableHeight == last ? tableHeight : (tableHeight - last  / tableHeight) - 1);
            int downX = (last % tableWidth == last ? tableWidth : (tableWidth - last % tableWidth) - 1);
            List<KeyValuePair<int,int>> abadonCells = new List<KeyValuePair<int, int>>();
            for(int xa = tableWidth  - 1, iter = last; ;xa--)
            {
                for(int ya = tableHeight - 1; ya >= 0; ya--)
                {
                    abadonCells.Add(new KeyValuePair<int,int>(ya,xa));
                    last--;
                    if (last == 0) break;
                }
                if (last == 0) break;
            }

            while (true)
            {
                if (x < tableHeight)
                {
                    if (abadonCells.Where(z => z.Key == x && z.Value == y).Count() == 0)
                        table[y, x++] = input[l++];
                    else
                    {
                        x++;
                    }
                }
                else
                {
                    x++;
                }

                while (x != 0)
                {
                    if (l == text.Length) break;

                    if (y < tableWidth && x < tableHeight)
                    {
                        if (abadonCells.Where(z => z.Key == x && z.Value == y).Count() == 0)
                            table[y++, x--] = input[l++];
                        else
                        {
                            y++; x--;
                        }
                    }
                    else
                    {
                        y++; x--;
                    }
                }


                if (y < tableWidth)
                {
                    if (abadonCells.Where(z => z.Key == x && z.Value == y).Count() == 0)
                        table[y++, x] = input[l++];
                    else
                    {
                        y++;
                    }
                }
                else
                {
                    y++;
                }

                while (y != 0)
                {
                    if (l == text.Length) break;

                    if (y < tableWidth && x < tableHeight)
                    {
                        if (abadonCells.Where(z => z.Key == x && z.Value == y).Count() == 0)
                            table[y--, x++] = input[l++];
                        else
                        {
                            y--; x++;
                        }
                    }
                    else
                    {
                        y--; x++;
                    }
                }
                if (l == text.Length) break;
            }


            return table;
        }
    }
}

