using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class EntropyChecker
    {
        private string alphabetName;
        private string alphabet;
        private double alphabetEntropy = 0;


        public EntropyChecker()
        {
        }


        public EntropyChecker(string alphabet, double alphabetEntropy, string alphabetName)
        {
            Alphabet = alphabet;
            AlphabetEntropy = alphabetEntropy;
            AlphabetName = alphabetName;
        }

        private int myVar;

        public string Alphabet
        {
            get { return alphabet; }
            set { alphabet = value; }
        }


        public string AlphabetName
        {
            get { return alphabetName; }
            set { alphabetName = value; }
        }


        public double AlphabetEntropy
        {
            get { return alphabetEntropy; }
            set { alphabetEntropy = value; }
        }


        public Dictionary<char, int> alphabetListToDictionary()
        {
            Dictionary<char, int> dict = new Dictionary<char, int>(Alphabet.Length);
            foreach (char x in alphabet)
            {
                dict.Add(x, 0);
            }
            return dict;
        }

        public string GetAllText(string text, StreamReader reader)
        {
            if (reader == null)
            {
                throw new Exception("Document isn't open");
            }
            else
            {
                return reader.ReadToEnd();
            }
        }

        public Dictionary<char, double> getSymbolsChances(string text, Dictionary<char, int> counts)
        {
            Dictionary<char, double> chances = new Dictionary<char, double>(this.alphabet.Length);

            for (int i = 0; i < counts.Count; i++)
            {
                chances.Add(this.alphabet[i], (double)counts[this.alphabet[i]] / text.Length);
            }

            return chances;
        }

        public void getSymbolsCounts(string text, Dictionary<char, int> alphabet)
        {
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < this.alphabet.Length; j++)
                {
                    if (text[i] == this.alphabet[j])
                    {
                        alphabet[this.alphabet[j]]++;
                    }
                }
            }
        }


        public void computeTextEntropy(Dictionary<char, double> chances)
        {
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (chances[alphabet[i]] != 0)
                {
                    AlphabetEntropy += chances[alphabet[i]] * Math.Log(chances[alphabet[i]], 2);
                }
            }

            AlphabetEntropy = -AlphabetEntropy;
        }

        public double computeTextEntropyWithError(Dictionary<char, double> chances, double p)
        {
            double q = (double)1 - p;
            double entropy = 0;
            double conditionalEntropy = 1 - ((-p * Math.Log(p, 2)) - (q * Math.Log(q, 2)));
            if (double.IsNaN(conditionalEntropy))
            {
                return 0;
            }
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (chances[alphabet[i]] != 0)
                {
                    entropy += ((chances[alphabet[i]] * Math.Log(chances[alphabet[i]], 2)) - conditionalEntropy);
                }
            }

            return -entropy;
        }

        public StreamReader OpenDocument(string path)
        {
            return new StreamReader(path);
        }

        public void printAlphabet()
        {
            Console.WriteLine($"\nАлфавит {this.AlphabetName}:"); ;
            foreach (char x in this.alphabet)
            {
                Console.Write(x); Console.Write(" ");
            }

        }

        public void printChances(Dictionary<char, double> chances)
        {
            Console.WriteLine("\nШансы появления символа:");
            foreach (char x in this.Alphabet)
                Console.WriteLine($"{x} : {chances[x]}");
        }

        public void printAlhabetEntropy()
        {
            Console.WriteLine($"\nЭнтропия алфавита для языка '{this.AlphabetName}' равна {this.AlphabetEntropy}.");
        }
    }
}
