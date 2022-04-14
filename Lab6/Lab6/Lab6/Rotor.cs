using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Rotor
    {
        public char[] Alphabet { get; set; }
        public int Shift { get; set; }
        public int countOfShifts { get; set; }
        public int DoShift(int turnsCount)
        {
            int _turnsCount = 0;
            if (countOfShifts < Alphabet.Length)
            {
                countOfShifts += Shift;
            }
            else
            {
                countOfShifts = Alphabet.Length - countOfShifts;
                _turnsCount++;
            }

            for (int j = 0; j < Shift + turnsCount; j++)
            {
                char temp = ' ';
                for (int i = 0; i < Alphabet.Length - 1; i++)
                {
                    if (i == 0)
                    {
                        temp = Alphabet[Alphabet.Length - 1];
                        Alphabet[Alphabet.Length - 1] = Alphabet[i];
                        Alphabet[i] = Alphabet[i + 1];
                    }
                    else
                    {
                        Alphabet[i] = Alphabet[i + 1];
                    }
                    if (i == Alphabet.Length - 2)
                    {
                        Alphabet[Alphabet.Length - 2] = temp;
                    }
                }
            }
            return _turnsCount;
        }


        public void PickStartPosition(string startPositionAlpha)
        {
            char chr = Char.Parse(startPositionAlpha.Replace("System.Windows.Controls.ListBoxItem:", "").Trim());
            int oldIndex = Array.IndexOf(Alphabet, chr);
            for (int j = 0; j < oldIndex; j++)
            {
                char temp = ' ';
                for (int i = 0; i < Alphabet.Length - 1; i++)
                {
                    if (i == 0)
                    {
                        temp = Alphabet[Alphabet.Length - 1];
                        Alphabet[Alphabet.Length - 1] = Alphabet[i];
                        Alphabet[i] = Alphabet[i + 1];
                    }
                    else
                    {
                        Alphabet[i] = Alphabet[i + 1];
                    }
                    if (i == Alphabet.Length - 2)
                    {
                        Alphabet[Alphabet.Length - 2] = temp;
                    }
                }
            }
        }
    }
}
