using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class MultipleReplacement
    {
        private List<KeyValuePair<int,char>> keyHorizontal;
        private List<KeyValuePair<int, char>> keyVertical;
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public List<KeyValuePair<int, char>> KeyVertical
        {
            get { return keyVertical; }
            set { keyVertical = value; }
        }

        public List<KeyValuePair<int, char>> KeyHorizontal
        {
            get { return keyHorizontal; }
            set { keyHorizontal = value; }
        }

        public MultipleReplacement()
        {
        }

        public void printKeys()
        {
            Console.Write($" ");
            foreach (KeyValuePair<int,char> x in keyHorizontal)
            {
                Console.Write($"{x.Value,5}");
            }
            Console.WriteLine($"");
            Console.Write($" ");
            foreach (KeyValuePair<int, char> x in keyHorizontal)
            {
                Console.Write($"{x.Key,5}");
            }
            Console.WriteLine($"");

            foreach (KeyValuePair<int, char> x in keyVertical)
            {
                Console.Write($"{x.Value,2}");
                Console.WriteLine($"{x.Key,2}");
            }
        }

        public MultipleReplacement(string text, List<KeyValuePair<int, char>> keyVertical, List<KeyValuePair<int, char>> keyHorizontal)
        {
            if(keyVertical.Count * keyHorizontal.Count >= text.Length)
            {
                Text = text;
            }
            else
            {
                throw new Exception("Сообщение слишком длинное для данных ключей!");
            }
            KeyVertical = keyVertical;
            KeyHorizontal = keyHorizontal;
        }

        public void printMatrix(char[,] input)
        {
            int tableWidth = keyVertical.Count;
            int tableHeight = KeyHorizontal.Count;
            for (int w = 0; w < tableWidth; w++)
            {
                for (int i = 0; i < tableHeight; i++)
                {
                    Console.Write($"{(input[w, i] == ' ' || input[w, i] == '\0'? '*' : input[w, i]),5}");
                }
                Console.Write($"\n");

            }
            Console.Write($"\n");
        }

        public char[,] createMatrix(string input)
        {
            int tableWidth = keyVertical.Count;
            int tableHeight = KeyHorizontal.Count;
            char[,] table = new char[tableWidth, tableHeight];
            int l = 0;
            for (int w = 0; w < tableWidth; w++)
            {
                for (int i = 0; i < tableHeight; i++)
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
            int tableWidth = keyVertical.Count;
            int tableHeight = KeyHorizontal.Count;
            StringBuilder stringBuilder = new StringBuilder();
            for (int w = 0; w < tableWidth; w++)
            {
                for (int l = 0; l < tableHeight; l++)
                    stringBuilder.Append(input[w, l]);
            }
            return stringBuilder.ToString();
        }

        public string Encrypt()
        {
            char[,] table = createMatrix(Text);

            int tableWidth = keyVertical.Count;
            int tableHeight = KeyHorizontal.Count;

            char[,] result = new char[tableWidth, tableHeight];
            int iteration = 0; 
            while(iteration++ < tableHeight )
            {
                int k = 0;
                for (int y = 0; y < tableWidth; y++)
                {
                    k = keyHorizontal.IndexOf(keyHorizontal.Where(l => l.Key == iteration).First());
                    result[y,iteration - 1] = table[y,k];

                }
            }
            Console.WriteLine("После горизонтальной перестановки:\n");
            printMatrix(result);

            char[,] resultV = new char[tableWidth, tableHeight];

            iteration = 0;
            while (iteration++ < tableWidth)
            {
                int k = 0;
                for (int y = 0; y < tableHeight ; y++)
                {
                    k = keyVertical.IndexOf(keyVertical.Where(l => l.Key == iteration).First());
                    resultV[iteration - 1,y] = result[k,y];

                }
            }

            Console.WriteLine("После вертикальной перестановки:\n");
            printMatrix(resultV);

            return createString(resultV);
        }
    }
}
